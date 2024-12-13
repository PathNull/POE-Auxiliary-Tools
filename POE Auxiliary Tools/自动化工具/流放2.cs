using AutoHotkey.Interop;
using Path_of_Exile_Tool.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace POE_Auxiliary_Tools.自动化工具
{
    public partial class 流放2 : Form
    {
        private AutoHotkeyEngine ahk;
        private List<BloodPos> bloodPosList = new List<BloodPos>();
        private List<BloodPos> bluePosList = new List<BloodPos>();
        public 流放2()
        {
            InitializeComponent();
            ahk = AutoHotkeyEngine.Instance;
        }

        private void 流放2_Load(object sender, EventArgs e)
        {
            string directory = Application.StartupPath;
            var loadPath_blood = directory + "\\AHK Script\\自动吃药2.ahk";
            var loadPath_blue = directory + "\\AHK Script\\自动吃藍2.ahk";
            ahk.LoadFile(loadPath_blood);
            ahk.LoadFile(loadPath_blue);
            bloodPosList.Add(new BloodPos { percentage = "80%", PosX = "87", PosY = "939", Color = "0x403E8B" });
            bloodPosList.Add(new BloodPos { percentage = "35%", PosX = "79", PosY = "1008", Color = "0x252258" });

            bluePosList.Add(new BloodPos { percentage = "20%", PosX = "1748", PosY = "1011", Color = "0x613622" });
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            BloodPos obj;
            var isStartUp = ce_zdcx.Checked;
            var threshold = cb_cfxl.SelectedItem.ToString();
            var frequency = cb_jcpl.SelectedItem.ToString();
            var jgTime = cb_cxjg.SelectedItem.ToString();  //吃血后暂停检测的时间
            obj = bloodPosList.Find(X => X.percentage == threshold);

            ahk.SetVar("posX", obj.PosX);
            ahk.SetVar("posY", obj.PosY);
            ahk.SetVar("color", obj.Color);
            ahk.SetVar("frequency", frequency);
            ahk.SetVar("jgTime", jgTime);
            ahk.ExecFunction("WAutoBloodBottle", isStartUp.ToString(), frequency);
            //ahk.ExecFunction("WAutoBloodBottle", isStartUp.ToString());
        }

        private void ce_zdcl_CheckedChanged(object sender, EventArgs e)
        {
            BloodPos obj;
            var isStartUp = ce_zdcl.Checked;
            var threshold = cb_cfll.SelectedItem.ToString();
            var frequency = cb_jcpl2.SelectedItem.ToString();
            var jgTime = cb_cljg.SelectedItem.ToString();
            obj = bluePosList.Find(X => X.percentage == threshold);
            ahk.SetVar("posX2", obj.PosX);
            ahk.SetVar("posY2", obj.PosY);
            ahk.SetVar("color2", obj.Color);
            ahk.SetVar("frequency2", frequency);
            ahk.SetVar("jgTime2", jgTime);
            ahk.ExecFunction("WAutoBlueBottle", isStartUp.ToString(), frequency);
        }
    }
}
