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
}