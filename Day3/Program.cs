var lines = File.ReadLines(@"E:\Dev\AdventOfCode_2023\Day3\input.txt").ToList();

//fuck: in the moment i finished this nonsense i had an idea for a way better solution

var charArray = new char[lines.Count, lines.Select(str => str.Length).Max()];

for (var i = 0; i < lines.Count; i++)
{
    for (var j = 0; j < lines[i].Length; j++)
        charArray[i, j] = lines[i][j];
}

var sum = 0;

for (var i = 0; i < charArray.GetLength(0); i++)
{
    var currentNumber = string.Empty;
    var adjacentSymbolFound = false;

    for (var j = 0; j < charArray.GetLength(1); j++)
    {
        var entry = charArray[i, j];

        if (char.IsDigit(entry))
        {
            //only if we didn't already find a adjacent symbol
            if (!adjacentSymbolFound)
            {
                //check if there is a adjacent symbol;
                bool checkRightIsLegit = false,
                    checkLeftIsLegit = false,
                    checkTopIsLegit = false,
                    checkBottomIsLegit = false;

                //check to the right
                if (j + 1 < charArray.GetLength(1))
                {
                    if (IsSymbol(charArray[i, j + 1]))
                        adjacentSymbolFound = true;

                    checkRightIsLegit = true;
                }

                //check to the left
                if (j - 1 > 0)
                {
                    if (IsSymbol(charArray[i, j - 1]))
                        adjacentSymbolFound = true;

                    checkLeftIsLegit = true;
                }

                //check to the top
                if (i - 1 > 0)
                {
                    if (IsSymbol(charArray[i - 1, j]))
                        adjacentSymbolFound = true;

                    checkTopIsLegit = true;
                }

                //check to the bottom
                if (i + 1 < charArray.GetLength(0))
                {
                    if (IsSymbol(charArray[i + 1, j]))
                        adjacentSymbolFound = true;

                    checkBottomIsLegit = true;
                }

                //check top left
                if (checkTopIsLegit && checkLeftIsLegit)
                {
                    if (IsSymbol(charArray[i - 1, j - 1]))
                        adjacentSymbolFound = true;
                }

                //check top right
                if (checkTopIsLegit && checkRightIsLegit)
                {
                    if (IsSymbol(charArray[i - 1, j + 1]))
                        adjacentSymbolFound = true;
                }

                //check bottom left
                if (checkBottomIsLegit && checkLeftIsLegit)
                {
                    if (IsSymbol(charArray[i + 1, j - 1]))
                        adjacentSymbolFound = true;
                }

                //check bottom right
                if (checkBottomIsLegit && checkRightIsLegit)
                {
                    if (IsSymbol(charArray[i + 1, j + 1]))
                        adjacentSymbolFound = true;
                }
            }
            
            currentNumber += entry;

            //check if the current number is finished
            var currentNumberIsFinished = false;
            if (j + 1 < charArray.GetLength(1))
            {
                if (!char.IsDigit(charArray[i, j + 1]))
                    currentNumberIsFinished = true;
            }
            else
                currentNumberIsFinished = true;

            //if the current number is finished we have to reset stuff
            if (currentNumberIsFinished)
            {
                //if currentNumberIsFinished and we found a symbol around the number we add to the sum
                if (adjacentSymbolFound)
                    sum += int.Parse(currentNumber);

                //reset stuff - because there could be more cases on the line
                currentNumber = string.Empty;
                adjacentSymbolFound = false;
            }
        }
    }
}

Console.WriteLine(sum);

bool IsSymbol(char c) => !char.IsDigit(c) && c != '.';