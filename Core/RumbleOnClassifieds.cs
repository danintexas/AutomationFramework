using System;

class RumbleOnClassifieds
{
    public static void PhotoSelection()
    {
        System.Windows.Forms.SendKeys.SendWait(@"C:\Automation Logs\Right Side.png");
        System.Windows.Forms.SendKeys.SendWait("{ENTER}");
    }
}
