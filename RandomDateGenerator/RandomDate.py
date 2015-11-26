# Cameron Taylor
# Random Date Generator

from random import randint


def getRandMonth():
    month = randint(1, 12)
    year = randint(0, 54) + 1961
    day = 0

    _31days = [1, 3, 5, 7, 8, 10, 12]
    _30days = [4, 6, 9, 11]

    if(month in _31days):
        day = randint(0, 31)
    elif(month in _30days):
        day = randint(0, 30)
    else:
        if(year % 4 is 0):
            day = randint(0, 29)
        else:
            day = randint(0, 28)

    return str(month) + "-" + str(day) + "-" + str(year)

print(getRandMonth())
