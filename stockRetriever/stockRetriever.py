import urllib.request
import time
import threading


configFile = "settings.csv"
coinListFile = "coinList.csv"
endProgram = False
pollSleepTime_Sec = 10
coins = []

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
        if line.startswith("#") == False and len(lineSplit) == 5:
            coin = {
                    'name': lineSplit[0],
                    'filePrefix': lineSplit[1],
                    'fileSuffix': lineSplit[2],
                    'url': lineSplit[3],
                    'marker': lineSplit[4]
            }
            coin['filename'] = coin['filePrefix'] + coin['name'] + coin['fileSuffix']
            coins.append(coin)
            print("Gathering price for " + coin['name'] + " using marker " + coin['marker'])
        # lineSplit.len = 3
    # for each line
# end ReadCoinList()

#
# Thread to grab prices for all urls
#
def GetPriceThread():
    global exportFile, coins
    print("GetPriceThread Start\n")

    for coin in coins:
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

            # if the text is in the data
            textMarker = coin['marker']
            if textMarker in dataStr:
                startIndex = dataStr.index(textMarker)

                # Get the index of the start of the actual price
                sub = dataStr[startIndex:]
                startIndex = sub.index("$") + 1

                # Get the index of the end of the actual price
                endIndex = sub.index("<")
                price = sub[startIndex:endIndex]
                price = price.replace(",", "")
                outputLine += price
            else:
                print("Marker not found for " + coin['name'])
            # if marker in text

            print(coin['name'] + ": " + outputLine)
            fs.write(outputLine + "\n")
            fs.close()
        # for url in urls

        if endProgram != True:
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