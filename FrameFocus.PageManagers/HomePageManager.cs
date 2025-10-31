using DataManager;
using ErrorLogging;
using FrameFocus.PageManagers.DTOs;

namespace FrameFocus.PageManagers;

public class HomePageManager {
    public RecentDataDTO RecentData { get; set; } = new RecentDataDTO();
    public DataBackgroundDTO DataBackground { get; set; } = new DataBackgroundDTO();
    public OverallDataDTO OverallData { get; set; } = new OverallDataDTO();

    private readonly DataValueManager _dataManager = new();

    public HomePageManager()
    {
        SetBackgroundColours();
    }

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

    private static string AssignCssString (float value){
        return value switch {
            -1 => "recentPanelsBelow",
            0 => "recentPanels",
            1 => "recentPanelsAbove",
            _ => "recentPanels",
        };
    }
}