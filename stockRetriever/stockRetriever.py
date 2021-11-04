import urllib.request
import time
import threading
import os
import os.path
import enum

configFile = "settings.csv"
coinListFile = "coinList.csv"
endProgram = False
pollSleepTime_Sec = 10
coins = []

class Days(enum.Enum):
   Price = 0
   MarketCap = 1
   Volume = 2
   CirculatingSupply = 3

#
# Read Config
#
def ReadConfig():
    global pollSleepTime_Sec
    print("Reading Config file")

    pollHours = 0
    pollMinutes = 0
    pollSeconds = 0

    fs = open(configFile, "r")
    for line in fs.readlines():
        lineSplit = line.split(",")
        if "pollHours" in lineSplit[0]:
            pollHours = int(lineSplit[1])
        elif "pollMinutes" in lineSplit[0]:
            pollMinutes = int(lineSplit[1])
        elif "pollSeconds" in lineSplit[0]:
            pollSeconds = int(lineSplit[1])
    # end for line in lines

    pollSleepTime_Sec = (pollHours * 60 * 60) + (pollMinutes * 60) + pollSeconds

    print("Poll every " + str(pollSleepTime_Sec) + " seconds")
# end ReadConfig()

#
# Read Coin List
#
def ReadCoinList():
    global coins
    print ("Reading Coin List")

    coins = []

    fs = open(coinListFile, "r")
    for line in fs.readlines():
        line = line.replace("\n", "")
        lineSplit = line.split(",")
        if line.startswith("#") == False and len(lineSplit) >= 4:
            coin = {
                'name': lineSplit[0],
                'filePrefix': lineSplit[1],
                'fileSuffix': lineSplit[2],
                'url': lineSplit[3],
                'markers' : [
                    #lineSplit[4],
                    #lineSplit[5],
                    #lineSplit[6],
                    #lineSplit[7]
                ]
            }

            markerLength = len(lineSplit) - 4
            for i in range(markerLength):
                markerSplit = lineSplit[i + 4].split(":")
                markerObject  = {
                    'enumIndex' : i,
                    'divMarker' : markerSplit[0],
                    'valueMarker' : markerSplit[1]
                }
                coin['markers'].append(markerObject)

            coin['filename'] = coin['filePrefix'] + coin['name'] + coin['fileSuffix']
            coins.append(coin)
            print("Gathering price for " + coin['name'])
        # lineSplit.len = 3
    # for each line
# end ReadCoinList()

#
# Git auto push
#
def GitAutoPush():
    print("Git Add")
    os.system("git add .")
    print("Git Commit")
    os.system("git commit -m \"Auto update\"")
    print("Git Push")
    os.system("git push")

#
# Thread to grab prices for all urls
#
def GetPriceThread():
    global exportFile, coins
    print("GetPriceThread Start\n")

    for coin in coins:
        if not os.path.exists(coin['filename']):
            fs = open(coin['filename'], "w", encoding="utf-8")
            # write out the header
            fs.write("year,month,day,hour,minute,second,")
            fs.close()

    while endProgram != True:
        # Get the current timestamp
        year = str(time.localtime().tm_year)
        month = str(time.localtime().tm_mon)
        day = str(time.localtime().tm_mday)
        hour = str(time.localtime().tm_hour)
        minute = str(time.localtime().tm_min)
        sec = str(time.localtime().tm_sec)
        timestamp = year + "," + month + "," + day + "," + hour + "," + minute + "," + sec + ","
        
        # for each url
        for coin in coins:
            outputLine = timestamp
            fs = open(coin['filename'], "a", encoding="utf-8")
            # Get the html data
            webUrl = urllib.request.urlopen(coin['url'])
            data = webUrl.read()
            dataStr = data.decode("utf8")

            for marker in coin['markers']:
                if marker['divMarker'] in dataStr:
                    startIndex = dataStr.index(marker['divMarker'])

                    # Get the index of the start of the actual value
                    sub = dataStr[startIndex:]
                    startIndex = sub.index(marker['valueMarker']) + len(marker['valueMarker'])
                    
                    # Get the index of the end of the actual value
                    endIndex = sub.index("<", startIndex + 1)
                    value = sub[startIndex:endIndex]
                    value = value.replace(",", "")
                    outputLine += value + ","
                    

            ## if the text is in the data
            #textMarker = coin['marker']
            #if textMarker in dataStr:
            #    startIndex = dataStr.index(textMarker)
#
            #    # Get the index of the start of the actual price
            #    sub = dataStr[startIndex:]
            #    startIndex = sub.index("$") + 1
#
            #    # Get the index of the end of the actual price
            #    endIndex = sub.index("<")
            #    price = sub[startIndex:endIndex]
            #    price = price.replace(",", "")
            #    outputLine += price
            #else:
            #    print("Marker not found for " + coin['name'])
            ## if marker in text

            print(coin['name'] + ": " + outputLine)
            fs.write(outputLine + "\n")
            fs.close()
        # for url in urls

        if endProgram != True:
            GitAutoPush()
            print("*****")
            time.sleep(pollSleepTime_Sec)
    # while !endProgram
# GetPriceThread()

#
# Main
#
ReadConfig()
ReadCoinList()

thGetPrice = threading.Thread(target=GetPriceThread, args=())
thGetPrice.start()

while endProgram != True:
    cmd = input()
    if "q" in cmd:
        endProgram = True
        print("Ending Program")
# while !endProgram

# end Main     