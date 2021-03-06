import urllib.request
import time
import threading
import os
import os.path
import sys

configFile = "settings.csv"
investmentListFile = "myList.csv"
endProgram = False
gitPushSleepTime_Sec = 60 * 60
investments = []

#
# ParseStringToInt
#
def ParseStringToInt(s):
    successful = False
    try:
        value = int(s)
        successful = True
    except ValueError:
        value = 0
    return value, successful
# ParseStringToInt()

#
# Read Investments List
#
def ReadInvestmentsList():
    global investments
    print ("Reading Investments List")

    investments = []

    fs = open(investmentListFile, "r")
    for line in fs.readlines():
        line = line.replace("\n", "")
        lineSplit = line.split(",")
        if line.startswith("#") == False and len(lineSplit) >= 4:
            investment = {
                'name': lineSplit[0],
                'filePrefix': lineSplit[1],
                'fileSuffix': lineSplit[2],
                #'fetchFrequencyInMinutes': lineSplit[3],
                'url': lineSplit[4],
                'markers' : []
            }

            # Parse the frequency in minutes and convert to seconds
            investment['fetchFrequency_sec'], successful = ParseStringToInt(lineSplit[3])
            if successful != True:
                investment['fetchFrequency_sec'] = 60
                print("Fetch Frequency parse error, defaulting to 1 minute")
            else:
                investment['fetchFrequency_sec'] *= 60

            markerLength = len(lineSplit) - 5
            for i in range(markerLength):
                markerSplit = lineSplit[i + 5].split(":")
                markerObject  = {
                    'index' : i,
                    'markerName' : markerSplit[0],
                    'startMarker' : markerSplit[1],
                    'valueMarker' : markerSplit[2],
                    'endMarker' : markerSplit[3]
                }
                investment['markers'].append(markerObject)

            investment['filename'] = investment['filePrefix'] + investment['name'] + investment['fileSuffix']
            investments.append(investment)
            print("Gathering price for " + investment['name'])
        # lineSplit.len = 3
    # for each line
# end ReadInvestmentsList()

#
# CreateDailyInvestmentFile
#
def CreateDailyInvestmentFile(investment, filename):
    year = str(time.localtime().tm_year).zfill(4)
    month = str(time.localtime().tm_mon).zfill(2)
    day = str(time.localtime().tm_mday).zfill(2)

    # Create the Year directory if it doesn't exist
    dir = year
    if not os.path.exists(dir):
        os.mkdir(dir)

    # Create the Month directory if it doesn't exist
    dir = year + "/" + month
    if not os.path.exists(dir):
        os.mkdir(dir)

    # Create the day's directory if it doesn't exist
    dir = year + "/" + month + "/" + year + "-" + month + "-" + day + "/"
    if not os.path.exists(dir):
        os.mkdir(dir)

    # Create the file in the directory if it doesn't exist
    filePath = dir + filename
    if not os.path.exists(filePath):
        fs = open(filePath, "w", encoding="utf-8")
        # write out the header
        header = "Date-Time,"
        for marker in investment['markers']:
            header += marker['markerName'] + ","
        header = header[0:len(header) - 1]
        fs.write(header + "\n")
        fs.close()
    
    return filePath
# CreateDailyInvestmentFile()

#
# Thread to grab price for an investment
#
def GetPriceThread(investment):
    print("GetPriceThread: " + investment['name'] + " Start\n")

    while endProgram != True:
        try:
            # Get the current timestamp
            year = str(time.localtime().tm_year).zfill(4)
            month = str(time.localtime().tm_mon).zfill(2)
            day = str(time.localtime().tm_mday).zfill(2)
            hour = str(time.localtime().tm_hour).zfill(2)
            minute = str(time.localtime().tm_min).zfill(2)
            sec = str(time.localtime().tm_sec).zfill(2)
            timestamp = year + "-" + month + "-" + day + "T" + hour + ":" + minute + ":" + sec + ","
        
            outputLine = timestamp
        
            filePath = CreateDailyInvestmentFile(investment, investment['filename'])
            fs = open(filePath, "a", encoding="utf-8")
            # Get the html data
            webUrl = urllib.request.urlopen(investment['url'])
            data = webUrl.read()
            dataStr = data.decode("utf8")

            for marker in investment['markers']:
                if marker['startMarker'] in dataStr:
                    startIndex = dataStr.index(marker['startMarker'])

                    # Get the index of the start of the actual value
                    sub = dataStr[startIndex:]
                    startIndex = sub.index(marker['valueMarker']) + len(marker['valueMarker'])
                    
                    # Get the index of the end of the actual value
                    endIndex = sub.index(marker['endMarker'], startIndex + 1)
                    value = sub[startIndex:endIndex]
                    value = value.replace(",", "")

                    # if Shib Burn, the value is shifted right by 18 digits
                    if investment['name'] == 'Shib-Burn':
                        value = value[0:len(value) - 18]

                    outputLine += value + ","
                else:
                    outputLine += ","

            outputLine = outputLine[0:len(outputLine) - 1]
            print("      " + investment['name'] + ": " + outputLine)
            fs.write(outputLine + "\n")
            fs.close()
        except:
            print("      Exception occured with " + investment['name'])

        if endProgram != True:
            time.sleep(investment['fetchFrequency_sec'])
    # while !endProgram
# GetPriceThread()

#
# Git Push Thread
#
def GitPushThread():
    while endProgram != True:
        print("Git: Pull")
        os.system("git pull")
        print("Git: Add")
        os.system("git add .")
        print("Git: Commit")
        os.system("git commit -m \"(stockRetreiver) Auto update\"")
        print("Git: Push")
        os.system("git push")

        time.sleep(gitPushSleepTime_Sec)
        print("*****")
    # while !endProgram
# GitPushThread


#
# Main
#
ReadInvestmentsList()

thGitPush = threading.Thread(target=GitPushThread, args=())
thGitPush.start()

for investment in investments:
    thGetPrice = threading.Thread(target=GetPriceThread, args=[investment])
    thGetPrice.start()

while endProgram != True:
    cmd = input()
    if "q" in cmd:
        endProgram = True
        print("Ending Program")
# while !endProgram

# end Main     