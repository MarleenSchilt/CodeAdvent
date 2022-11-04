using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    public class Logic
    {
        /// <summary>
        /// Determine how many times an item is bigger than their predecessor.
        /// </summary>
        /// <param name="fileContent">The content in which the incrementing values must be found.</param>
        /// <returns>Returns an int to show how many times an item was bigger than their predecessor.</returns>
        public static int GetConsecutiveIncrementsCount(List<int> fileContent)
        {
            int incrementCounter = 0;
            for (int i = 0; i < (fileContent.Count - 1); i++)
            {
                var firstValue = fileContent[i];
                var secondValue = fileContent[i + 1];

                if (secondValue > firstValue)
                    incrementCounter++;
            }

            return incrementCounter;
        }

        /// <summary>
        /// Get the combined value of a sector. A sector is made up of a given number of consecutive values.
        /// </summary>
        /// <param name="fileContent">The content in which the sectors must be found.</param>
        /// <param name="index">The index where a sector starts.</param>
        /// <param name="sectorSize">The size of a sector. This indicates how many consecutive values are added up.</param>
        /// <returns>The combined value of the sector.</returns>
        private static int GetSectorValue(List<int> fileContent, int index, int sectorSize)
        {
            int result = 0;
            for (int j = 0; j < sectorSize; j++)
            {
                result += fileContent[index + j];
            }

            return result;
        }

        /// <summary>
        /// Determine how many sectors are bigger than their predecessor.
        /// The first sector is the cumulative of values at index 0+1+2, next sector is the cumulative of the values on index 1+2+3, etc.
        /// </summary>
        /// <param name="fileContent">The content in which the sectors must be found.</param>
        /// <param name="sectorSize">The size of the sector. This indicates how many consecutive values are added up.</param>
        /// <returns>Returns an int to show how many times a sector was bigger than their predecessor.</returns>
        public static int GetIncrementingSectorsCount(List<int> fileContent, int sectorSize)
        {
            int incrementCounter = 0;
            int currentSector = 0;

            for (int i = 0; i < (fileContent.Count - sectorSize); i++)
            {
                // Initialize the current sector only once
                if (currentSector == 0)
                    currentSector = GetSectorValue(fileContent, i, sectorSize);

                int nextSector = GetSectorValue(fileContent, i + 1, sectorSize);

                if (nextSector > currentSector)
                    incrementCounter++;

                // Store current sector value so next loop doesn't need to calculate this again
                currentSector = nextSector;
            }

            return incrementCounter;
        }
    }
}
