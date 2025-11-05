using FrameFocus.PageManagers.DTOs;

namespace FrameFocus.PageManagers;

public class HomePageManager {
    public DataBackgroundDTO DataBackground { get; set; } = new DataBackgroundDTO();

    //  This method is used to assign a string based off the supplied value.
    private static string AssignCssString (float value){
        const float belowValue = -1.0f;
        const float equalValue = 0.0f;
        const float aboveValue = 1.0f;

        if (value == belowValue){
            return "recentPanelsBelow";
        }

        if (value == equalValue){
            return "recentPanels";
        }

        if (value == aboveValue){
            return "recentPanelsAbove";
        }

        return "recentPanels";
    }
}