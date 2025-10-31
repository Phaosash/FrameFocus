using ErrorLogging;

namespace DataManager.Classes;

internal class DataCalculations {
    //  This method is used to compare two values, and returns 1 if valueA is greater than valueB,
    //  -1 if valueA is less than valueB, it returns 0 (zero) if they are equal.
    public static float CompareValues (float valueA, float valueB){
        return valueA.CompareTo(valueB);
    }

    //  This method is used to calculate the percentage based on the two supplied values.
    public static float CalculatePercentage (float part, float total){
        return (part / total) * 100.0f;
    }

    //  This method is used to calculate the average of the supplied data.
    public static float CalculateAverage (float valueA, float valueB){
        int roundingValue = 2;
        return (float)Math.Round(valueA / valueB, roundingValue);
    }

    //  This method returns the current handicap based off the supplied average value. The target average being 200 and
    //  assumes that the benchmark is 100% of 200.
    //  If the result is a negative number then it means the average is greater than the target.
    //  Example a supplied average of 180 should equal a 20 point handicap. An averge of 210  should return a value of -10;
    public static float CalculateHandicap (float average){
        float targetAve = 200.0f;
        return targetAve - average;
    }

    //  This method is used to calculate the series total off the supplied data.
    public static float CalculateFloatArrayTotal (float[] values){
        float errorCode = -1.0f;

        try {
            float total = 0;

            for (int i = 0; i < values.Length; i++){
                total += values[i];
            }

            return total;
        } catch (IndexOutOfRangeException ex){
            LoggingManager.Instance.LogError(ex, "Failed to calculate the total, an index was out of range.");
            return errorCode;
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to calculate the total, encountered an unexpected problem.");
            return errorCode;
        }
    }
}