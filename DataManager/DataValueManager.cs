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

    private void CompareHighGame (){
        try {
            float tempValue = DataCalculations.CompareValues(_recentStatisticsData.HighGame, _statisticsData.HighGame);

            if (tempValue == 1){
                _statisticsData.HighGame = _recentStatisticsData.HighGame;
            }
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "");
        }
    }
}