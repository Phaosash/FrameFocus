using DataManager;
using ErrorLogging;
using FrameFocus.PageManagers.DTOs;

namespace FrameFocus.PageManagers;

public class HomePageManager {
    public RecentDataDTO RecentData { get; set; } = new RecentDataDTO();
    public DataBackgroundDTO DataBackground { get; set; } = new DataBackgroundDTO();
    public OverallDataDTO OverallData { get; set; } = new OverallDataDTO();

    private readonly DataValueManager _dataManager = new();

    public HomePageManager (){
        SetBackgroundColours();
    }

    //  This method is used to set the data background DTO css style colours based on the result of a comparitive calculation
    //  performed on the backend.
    private void SetBackgroundColours (){
        try {
            DataBackground.AverageBackground = AssignCssString(_dataManager.CompareAverage());
            DataBackground.StrikeBackground = AssignCssString(_dataManager.CompareStrikePercentage());
            DataBackground.SpareBackground = AssignCssString(_dataManager.CompareSparePercentage());
            DataBackground.OpenFrameBackground = AssignCssString(_dataManager.CompareOpenPercentage());
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Encountered an unexpected problem setting the background colours.");
        }
    }

    //  This method is used to assign a string based off the supplied value. -1 means the value was below and the background should be red.
    //  0 means it was the same so the colour should be the default, 1 means that the value was greater and the panel should be green.
    //  If any other value is returned then it defaults to the default colour.
    private static string AssignCssString (float value){
        return value switch {
            -1 => "recentPanelsBelow",
            0 => "recentPanels",
            1 => "recentPanelsAbove",
            _ => "recentPanels",
        };
    }

    //  This method is used to set the values of the most recent data on the UI.
    private void SetRecentValues (){
        try {
            RecentData.Average = _dataManager.GetRecentAverage();
            RecentData.StrikePercentage = _dataManager.GetRecentStrikePercentage().ToString("n2") + " %";
            RecentData.SparePercentage = _dataManager.GetRecentSparePercentage().ToString("n2") + " %";
            RecentData.OpenFramePercentage = _dataManager.GetRecentOpenPercentage().ToString("n2") + " %";
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to set the recent data values.");
        }
    }

    //  This method is used to set the overall data on the UI.
    private void SetDataValues (){
        try {
            OverallData.Average = _dataManager.GetAverage();
            OverallData.StrikePercentage = _dataManager.GetStrikePercentage().ToString("n2") + " %";
            OverallData.SparePercentage = _dataManager.GetSparePercentage().ToString("n2") + " %";
            OverallData.OpenPercentage = _dataManager.GetOpenPercentage().ToString("n2") + " %";
            OverallData.Handicap = SetHandicapValue();
            OverallData.HighGame = _dataManager.GetHighGame();
            OverallData.HighSeries = _dataManager.GetSeriesValue();
            OverallData.HighAverage = _dataManager.GetHighAverage();
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to set the overall data values.");
        }
    }

    private float SetHandicapValue (){
        try {
            float handicap = _dataManager.GetHandicap();

            if (handicap < 0.0f){
                handicap = 0f;
            }

            return handicap;
        } catch (Exception ex){
            LoggingManager.Instance.LogError(ex, "Failed to set the handicap value.");
            return 0f;
        }
    }
}