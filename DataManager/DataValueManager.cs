using DataManager.Classes;
using DataManager.DTOs;
using ErrorLogging;

namespace DataManager;

public class DataValueManager {
    private StatisticsData _recentStatisticsData = new();
    private StatisticsData _statisticsData = new();
    private const float ComparisonError = -2.0f;

    //  This method is used to compare the averages between the different data sets.
    public float CompareAverage (){
        try {
            return DataCalculations.CompareValues(_recentStatisticsData.Average, _statisticsData.Average);
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Something went wrong while comparing the averages.");
            return ComparisonError;
        }
    }

    //  This method is used to compare the strike percentages between the different data sets.
    public float CompareStrikePercentage (){
        try {
            return DataCalculations.CompareValues(_recentStatisticsData.StrikePercentage, _statisticsData.StrikePercentage);
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Something went wrong while comparing the strike percentages.");
            return ComparisonError;
        }
    }

    //  This method is used to compare the spare percentages between the differnt data sets.
    public float CompareSparePercentage (){
        try {
            return DataCalculations.CompareValues(_recentStatisticsData.SparePercentage, _statisticsData.SparePercentage);
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Something went wrong while comparing the spare percentage.");

            return ComparisonError;
        }
    }

    //  This method is used to compare the open frame percentages between the different data sets.
    public float CompareOpenPercentage (){
        try {
            return DataCalculations.CompareValues(_recentStatisticsData.OpenFramePercentage, _statisticsData.OpenFramePercentage);
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Something went wrong while comparing the spare percentage.");

            return ComparisonError;
        }
    }

    //  This method returns the most recently entered average.
    public float GetRecentAverage (){
        return _recentStatisticsData.Average;
    }

    //  This method returns the most recent strike percentage.
    public float GetRecentStrikePercentage (){
        return _recentStatisticsData.StrikePercentage;
    }

    //  This method returns the most recent spare percentage.
    public float GetRecentSparePercentage (){
        return _recentStatisticsData.SparePercentage;
    }

    //  This method returns the most recent open frame percentage.
    public float GetRecentOpenPercentage (){
        return _recentStatisticsData.OpenFramePercentage;
    }

    //  This method returns the overall average.
    public float GetAverage (){
        return _statisticsData.Average;
    }

    //  This method returns the overall strike percentage
    public float GetStrikePercentage (){
        return _statisticsData.StrikePercentage;
    }

    //  This method returns the overall spare percentage
    public float GetSparePercentage (){
        return _statisticsData.SparePercentage;
    }

    //  This method returns the overall open frame percentage
    public float GetOpenPercentage (){
        return _statisticsData.SparePercentage;
    }

    //  This method returns the highest recorded single game score value.
    public float GetHighGame (){
        return _statisticsData.HighGame;
    }

    //  This method returns the highest recorded game series.
    public float GetSeriesValue (){
        return _statisticsData.HighSeries;
    }

    //  This method returns the highest recorded average for a series
    public float GetHighAverage (){
        return _statisticsData.HighAverage;
    }

    //  This method returns the current handicap
    public float GetHandicap (){
        return DataCalculations.CalculateHandicap(_statisticsData.Average);
    }
}