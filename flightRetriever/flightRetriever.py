import time
import threading
import os
import os.path
import sys
import json
import requests_html
import datetime

from requests_html import HTMLSession
from datetime import datetime, timedelta

endProgram = False
gitPushSleepTime_Sec = 60 * 60

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

def CreateDailyFlightFile(filename):
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
        fs.write("//Timestamp, ICAO, Latitude_deg, Longitude_deg, Heading_deg, Altitude_ft, GroundSpeed_kts, AircraftType, RegistrationNum, Departure, Arrival, FlightNumber1, FlightNumber2\n")
        fs.close()
    
    return filePath
# CreateDailyFlightFile()

def GetFlightData():
    print("GetFlightData: Start\n")
    s = HTMLSession()

    url = 'https://www.flightradar24.com/33.65,-118.42/11'
    dataUrl = 'https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=33.754,33.125,-119.585,-117.235&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1'
        
    while endProgram != True:
        startTime = datetime.now()

        print(str(startTime) + ": Start new session ")

        response = s.get(url, headers={'User-Agent': 'Mozilla/5.0 (X11; CrOS armv7l 13597.84.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.98 Safari/537.36'})
        #response.html.render()
        contInnerLoop = True

        while contInnerLoop == True:
            currentTime = datetime.now()
            if currentTime - startTime < timedelta(minutes=10):
                year = str(time.localtime().tm_year).zfill(4)
                month = str(time.localtime().tm_mon).zfill(2)
                day = str(time.localtime().tm_mday).zfill(2)
                hour = str(time.localtime().tm_hour).zfill(2)
                minute = str(time.localtime().tm_min).zfill(2)
                sec = str(time.localtime().tm_sec).zfill(2)
                timestamp = year + "-" + month + "-" + day + "T" + hour + ":" + minute + ":" + sec + ","

                response = s.get(dataUrl, headers={'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.0.0 Safari/537.36'})

                if response.status_code == 200:
                    print(str(currentTime) + ": Got Data")
                    jsonObj = response.json()

                    if 'full_count' in jsonObj:
                        del jsonObj['full_count']
                    if 'version' in jsonObj:
                        del jsonObj['version']
                    if 'stats' in jsonObj:
                        del jsonObj['stats']

                    for (k, v) in jsonObj.items():
                        if len(v) == 19:
                            icao = v[0]
                            lat = v[1]
                            long = v[2]
                            heading_deg = v[3]
                            alt_ft = v[4]
                            groundSpeed_kts = v[5]
                            aircraftType = v[8]
                            registrationNum = v[9]
                            departure = v[11]
                            arrival = v[12]
                            flightNumber1 = v[13]
                            flightNumber2 = v[16]

                            output = timestamp + ", "
                            output += str(icao) + ", "
                            output += str(lat) + ", "
                            output += str(long) + ", "
                            output += str(heading_deg) + ", "
                            output += str(alt_ft) + ", "
                            output += str(groundSpeed_kts) + ", "
                            output += str(aircraftType) + ", "
                            output += str(registrationNum) + ", "
                            output += str(departure) + ", "
                            output += str(arrival) + ", "
                            output += str(flightNumber1) + ", "
                            output += str(flightNumber2) + "\n"

                            filePath = CreateDailyFlightFile(str(flightNumber1) + "_" + str(flightNumber2))
                            fs = open(filePath, "a", encoding="utf-8")
                            #fs.write("//ICAO, Latitude_deg, Longitude_deg, Heading_deg, Altitude_ft, GroundSpeed_kts, AircraftType, RegistrationNum, Departure, Arrival, FlightNumber1, FlightNumber2")
                            fs.write(output)
                            fs.close()
                        else:
                            print("Malformed json key")


                
                    if endProgram != True:
                        time.sleep(2)
                else:
                    r = s.post(url=url, headers={'Connection':'close'})
                    print("\n" + str(currentTime) + ": !! Failed or timed out with status: " + response.status_code + " !!\n")
                    contInnerLoop = False
                    break
            else:
                r = s.post(url=url, headers={'Connection':'close'})
                print("\n" + str(currentTime) + ": !! 10 minute timer hit, starting over!!\n")
                contInnerLoop = False
                break
        # while !endProgram     ## If the session times out after ~15 and gets an error, break out
    # while !endProgram         ## Sets up the session
# GetFlightData()

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
        os.system("git commit -m \"(flightRetreiver) Auto update\"")
        print("Git: Push")
        os.system("git push")

        time.sleep(gitPushSleepTime_Sec)
        print("*****")
    # while !endProgram
# GitPushThread

#
# Main
#

thGitPush = threading.Thread(target=GitPushThread, args=())
thGitPush.start()

#thGetData = threading.Thread(target=GetFlightDataThread2, args=[])
#thGetData.start()
GetFlightData()


# end Main     