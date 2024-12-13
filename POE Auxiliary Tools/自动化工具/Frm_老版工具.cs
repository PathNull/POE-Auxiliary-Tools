using AutoHotkey.Interop;
using Path_of_Exile_Tool.Model;
using POE_Auxiliary_Tools;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace POE_Auxiliary_Tools
{
    public partial class Frm_�ϰ湤�� : BaseForm
    {
        private AutoHotkeyEngine ahk;
        private List<BloodPos> bloodPosList = new List<BloodPos>();
        private List<BloodPos> bloodPosList_2K = new List<BloodPos>();
        private object lastHotKey;  //�ϴ�ѡ����ȼ�������ע���ȼ�(����)
        private object lastHotKey_HC;  //�ϴ�ѡ����ȼ�������ע���ȼ����Զ��سǣ�
        public Frm_�ϰ湤��()
        {
            InitializeComponent();
            ahk = AutoHotkeyEngine.Instance;
            string model = "kf";

            string directory = Application.StartupPath;
            var path_kf = GetApplicationPath();
            var loadPath_reform = directory + "\\AHK Script\\����.ahk";
            var loadPath_map = directory + "\\AHK Script\\�Զ�ϴ��ͼ.ahk";
            var loadPath_blood = directory + "\\AHK Script\\�Զ���ҩ.ahk";
            var loadPath_continuityClick = directory + "\\AHK Script\\����.ahk";
            var loadPath_test = directory + "\\AHK Script\\����.ahk";
            var loadPath_hc = directory + "\\AHK Script\\һ�����Żس�.ahk";
            var loadPath_st = directory + "\\AHK Script\\ϴ˺ͼ��ͼ.ahk";
            ahk.LoadFile(loadPath_reform);
            ahk.LoadFile(loadPath_map);
            ahk.LoadFile(loadPath_blood);
            ahk.LoadFile(loadPath_continuityClick);
            ahk.LoadFile(loadPath_test);
            ahk.LoadFile(loadPath_hc);

            comboBox1.SelectedItem = comboBox1.Items[1];
            comboBox2.SelectedItem = comboBox2.Items[0];
            comboBox3.SelectedItem = comboBox3.Items[2];
            comboBox4.SelectedItem = comboBox4.Items[0];
            comboBox5.SelectedItem = comboBox5.Items[1];
            comboBox6.SelectedItem = comboBox6.Items[1];
            //��ʼ��Ѫ��������ɫ
            //1080P
            bloodPosList.Add(new BloodPos { percentage = "90%", PosX = "110", PosY = "884", Color = "0x292163" });
            bloodPosList.Add(new BloodPos { percentage = "80%", PosX = "109", PosY = "898", Color = "0x2B2381" });
            bloodPosList.Add(new BloodPos { percentage = "70%", PosX = "100", PosY = "931", Color = "0x2A19AF" });
            bloodPosList.Add(new BloodPos { percentage = "60%", PosX = "99", PosY = "928", Color = "0x2918AE" });
            bloodPosList.Add(new BloodPos { percentage = "50%", PosX = "104", PosY = "966", Color = "0x2116A4" });
            bloodPosList.Add(new BloodPos { percentage = "40%", PosX = "119", PosY = "1007", Color = "0x120A72" });
            bloodPosList.Add(new BloodPos { percentage = "30%", PosX = "119", PosY = "1025", Color = "0x120B5F" });
            bloodPosList.Add(new BloodPos { percentage = "20%", PosX = "120", PosY = "1032", Color = "0x150D62" });
            bloodPosList.Add(new BloodPos { percentage = "10%", PosX = "151", PosY = "1050", Color = "0x151548" });

            bloodPosList_2K.Add(new BloodPos { percentage = "90%", PosX = "151", PosY = "1179", Color = "0x29215D" });
            bloodPosList_2K.Add(new BloodPos { percentage = "80%", PosX = "144", PosY = "1194", Color = "0x2B2181" });
            bloodPosList_2K.Add(new BloodPos { percentage = "70%", PosX = "130", PosY = "1242", Color = "0x2911B4" });
            bloodPosList_2K.Add(new BloodPos { percentage = "60%", PosX = "139", PosY = "1280", Color = "0x2818B1" });
            bloodPosList_2K.Add(new BloodPos { percentage = "50%", PosX = "143", PosY = "1303", Color = "0x1E188E" });
            bloodPosList_2K.Add(new BloodPos { percentage = "40%", PosX = "144", PosY = "1328", Color = "0x160F71" });
            bloodPosList_2K.Add(new BloodPos { percentage = "30%", PosX = "138", PosY = "1349", Color = "0x170F7D" });
            bloodPosList_2K.Add(new BloodPos { percentage = "20%", PosX = "142", PosY = "1369", Color = "0x130C61" });
            bloodPosList_2K.Add(new BloodPos { percentage = "10%", PosX = "177", PosY = "1397", Color = "0x15124E" });
        }
        public void TriggerLoadEvent()
        {
            this.OnLoad(EventArgs.Empty);
        }
        /// <summary>
        /// �����Զ�����Ľű�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startReform_Click(object sender, EventArgs e)
        {
            GZ();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            resolutionRatio.SelectedIndex = 0;
            GetScale();

        }
        /// <summary>
        /// ��ȡ�ֱ��� 2560 1440
        /// </summary>
        private void GetScale()
        {
            if (resolutionRatio.SelectedIndex == 1)
            {
                //var scaleX = Convert.ToDouble(2560) / Convert.ToDouble(1920);
                //var scaleY = Convert.ToDouble(1440) / Convert.ToDouble(1080);
                //ahk.SetVar("scaleX", Math.Round(scaleX,2).ToString());
                //ahk.SetVar("scaleY", Math.Round(scaleY,2).ToString());
                ahk.SetVar("scaleX", "1");
                ahk.SetVar("scaleY", "1");
            }
            else
            {
                ahk.SetVar("scaleX", "1");
                ahk.SetVar("scaleY", "1");
            }

        }
        private void GZ()
        {

            try
            {
                if (reformAffix.Text == "")
                {
                    MessageBox.Show("��׺����Ϊ�գ�");
                    return;
                }
                if (textBox1.Text == "")
                {
                    MessageBox.Show("�����������Ϊ�գ�");
                    return;
                }
                string condition = reformAffix.Text;  //����Ĵ�׺
                bool isIncrease = isIncreaseStone.Checked; //�Ƿ�ʹ������ʯ
                int number = Convert.ToInt32(textBox1.Text); //�������
                //��ô�׺�����±�
                ahk.ExecFunction("LockWindow");
                Clipboard.SetText("start");
                Core.Copy(ahk, Clipboard.GetText(), 2);
                ahk.ExecFunction("ClickGZ"); //����
                int index = Core.GetAffixIndex(ahk, Clipboard.GetText(), 5);
                bool isExist = false;
                isExist = Core.IsExit(Core.Shear_Plate(), condition, index); //�ж��Ƿ��ϴ���ô�׺

                for (int i = 0; i < number; i++)
                {
                    if (!isExist)
                    {
                        ahk.ExecFunction("ClickGZ"); //����
                        Core.Copy(ahk, Clipboard.GetText(), 2);
                        List<string[]> result = Core.Shear_Plate();  //װ����Ϣת��������
                                                                     //�ж��Ƿ�ʹ������ʯ
                        if (isIncrease)
                        {
                            if (result[index].Length == 1)
                            {
                                ahk.ExecFunction("AddStone"); //����
                                Core.Copy(ahk, Clipboard.GetText(), 10);
                                result = Core.Shear_Plate();
                            }
                        }
                        isExist = Core.IsExit(result, condition, index); //�ж��Ƿ��ϴ���ô�׺
                    }
                    else
                    {
                        ahk.ExecRaw("MsgBox, ����ɹ�! %Clipboard%");
                        return;
                    }
                }
                ahk.ExecRaw("MsgBox, û�и������Ӧ��׺!");
            }
            catch (Exception)
            {
                GZ();
            }


        }
        private static string GetApplicationPath()
        {
            string path = Application.StartupPath;
            string folderName = String.Empty;
            while (folderName.ToLower() != "bin")
            {
                path = path.Substring(0, path.LastIndexOf("\\"));
                folderName = path.Substring(path.LastIndexOf("\\") + 1);
            }
            return path.Substring(0, path.LastIndexOf("\\") + 1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// ϴ��ͼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string para = textBox2.Text;  //Σ�յĴ�׺
            string quantity = textBox4.Text;  //��Ʒ����
            string isMapNail = checkBox2.Checked.ToString();
            ahk.ExecFunction("Main", para, quantity, isMapNail, "��ͼ");

        }
        /// <summary>
        /// �Զ���ҩ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs S)
        {
            BloodPos obj;
            var isStartUp = checkBox1.Checked;
            var threshold = comboBox1.SelectedItem.ToString();
            var frequency = comboBox2.SelectedItem.ToString();
            var jgTime = comboBox6.SelectedItem.ToString();  //��Ѫ����ͣ����ʱ��
            if (resolutionRatio.SelectedIndex == 0)
            {
                obj = bloodPosList.Find(X => X.percentage == threshold);
            }
            else
            {
                obj = bloodPosList_2K.Find(X => X.percentage == threshold);
            }

            ahk.SetVar("posX", obj.PosX);
            ahk.SetVar("posY", obj.PosY);
            ahk.SetVar("color", obj.Color);
            ahk.SetVar("frequency", frequency);
            ahk.SetVar("jgTime", jgTime);

            SetAJ();
            ahk.ExecFunction("WAutoBloodBottle", isStartUp.ToString(), frequency);
            //ahk.ExecFunction("WAutoBloodBottle", isStartUp.ToString());
        }
        /// <summary>
        /// �Զ�ѭ������
        /// </summary>
        private void AutoSkills()
        {
            var isStartUp = checkBox4.Checked;
            var hotKey = comboBox7.Text;
            var keys = "";
            var times = "";
            var q_time = textBox7.Text;
            var w_time = textBox8.Text;
            var e_time = textBox9.Text;
            var r_time = textBox10.Text;
            var t_time = textBox11.Text;
            var q = checkBox11.Checked;
            var w = checkBox12.Checked;
            var e = checkBox13.Checked;
            var r = checkBox14.Checked;
            var t = checkBox15.Checked;
            if (isStartUp)
            {
                if (q)
                {
                    keys += "q ";
                    times += q_time + " ";
                }
                if (w)
                {
                    keys += "w ";
                    times += w_time + " ";
                }
                if (e)
                {
                    keys += "e ";
                    times += e_time + " ";
                }
                if (r)
                {
                    keys += "r ";
                    times += r_time + " ";
                }
                if (t)
                {
                    keys += "t ";
                    times += t_time + " ";
                }
            }
            ahk.SetVar("keys", keys);
            ahk.SetVar("times", times);
            ahk.ExecFunction("AutoSkills", hotKey, isStartUp.ToString());
        }
        /// <summary>
        /// ���ú�ҩʱ�İ���
        /// </summary>
        private void SetAJ()
        {
            var isStartUp = checkBox1.Checked;
            var wAutoBloodParm = "";
            var q = checkBox5.Checked;
            var w = checkBox6.Checked;
            var e = checkBox7.Checked;
            var r = checkBox8.Checked;
            var t = checkBox9.Checked;
            if (isStartUp)
            {
                if (q)
                    wAutoBloodParm += "q ";
                if (w)
                    wAutoBloodParm += "w ";
                if (e)
                    wAutoBloodParm += "e ";
                if (r)
                    wAutoBloodParm += "r ";
                if (t)
                    wAutoBloodParm += "t ";
                ahk.SetVar("wAutoBloodParm", wAutoBloodParm);

            }
            else
            {
                ahk.SetVar("wAutoBloodParm", "");
            }
        }
        /// <summary>
        /// �Զ���ҩ���ٷֱȸ���ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var isStartUp = checkBox1.Checked;
            if (isStartUp)
            {
                var threshold = comboBox1.SelectedItem.ToString();
                var obj = bloodPosList.Find(X => X.percentage == threshold);
                ahk.SetVar("posX", obj.PosX);
                ahk.SetVar("posY", obj.PosY);
                ahk.SetVar("color", obj.Color);
            }
        }
        /// <summary>
        /// �޸��Զ���ҩƵ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isStartUp = checkBox1.Checked;
            if (isStartUp)
            {
                var frequency = comboBox2.SelectedItem.ToString();
                ahk.SetVar("frequencyfrequency", frequency);
            }
        }
        /// <summary>
        /// �л�������㰴��ӳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isContinuityClick = checkBox3.Checked;   //�Ƿ�����
            var hotKey = comboBox3.SelectedItem.ToString();   //�ȼ�
            ahk.SetVar("hotKey", hotKey);
            if (isContinuityClick)
            {
                //ע���ϴ��ȼ�
                ahk.ExecFunction("ContinuityClick", lastHotKey.ToString(), "False");

                ahk.ExecFunction("ContinuityClick", hotKey, isContinuityClick.ToString());
                lastHotKey = hotKey;
            }
        }
        //���á�ͣ������
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            var isContinuityClick = checkBox3.Checked;   //�Ƿ�����
            var clickFrequency = comboBox4.SelectedItem.ToString();//���Ƶ��
            ahk.SetVar("clickFrequency", clickFrequency);
            var hotKey = comboBox3.SelectedItem.ToString();   //�ȼ�
            ahk.SetVar("hotKey", hotKey);
            if (isContinuityClick)
            {
                ahk.ExecFunction("ContinuityClick", hotKey, isContinuityClick.ToString());
                lastHotKey = comboBox3.SelectedItem.ToString();
            }
            else
            {
                ahk.ExecFunction("ContinuityClick", hotKey, isContinuityClick.ToString());
            }

        }
        /// <summary>
        /// �л��������Ƶ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isContinuityClick = checkBox3.Checked;   //�Ƿ�����
            var clickFrequency = comboBox4.SelectedItem.ToString();//���Ƶ��
            if (isContinuityClick)
            {
                ahk.SetVar("clickFrequency", clickFrequency);
            }
        }
        /// <summary>
        /// ϴ��־
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string quantity = textBox3.Text.ToString();  //��Ʒ����
            string isMapNail = checkBox2.Checked.ToString();
            ahk.ExecFunction("Main", "", quantity, "False", "��־");
            //ahk.ExecFunction("Test");

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            var isStart = checkBox4.Checked;
            ahk.ExecFunction("WAutoDabaoPotion", isStart.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("��׺����Ϊ�գ�");
                return;
            }
            if (textBox6.Text == "")
            {
                MessageBox.Show("��������Ϊ�գ�");
                return;
            }
            string condition = textBox5.Text;  //����Ĵ�׺
            int number = Convert.ToInt32(textBox6.Text); //�������
            //��ô�׺�����±�
            ahk.ExecFunction("LockWindow");
            Clipboard.SetText("start");
            Core.Copy(ahk, Clipboard.GetText(), 2);
            ahk.ExecFunction("ClickHD"); //����
            int index = 3;
            bool isExist = false;
            isExist = Core.IsExit(Core.Shear_Plate(), condition, index); //�ж��Ƿ��ϴ���ô�׺
            for (int i = 0; i < number; i++)
            {
                if (!isExist)
                {
                    ahk.ExecFunction("ClickHD"); //����
                    Core.Copy(ahk, Clipboard.GetText(), 2);
                    List<string[]> result = Core.Shear_Plate();  //װ����Ϣת��������
                    isExist = Core.IsExit(result, condition, index); //�ж��Ƿ��ϴ���ô�׺
                }
                else
                {
                    ahk.ExecRaw("MsgBox, �ɹ�! %Clipboard%");
                    return;
                }
            }
            ahk.ExecRaw("MsgBox, û�и������Ӧ��׺!");

        }
        /// <summary>
        /// ���ú�ҩʱ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox10_CheckedChanged(object sender, EventArgs s)
        {
            var isAutoHC = checkBox10.Checked;   //�Ƿ�����
            var hotKeyHC = comboBox5.SelectedItem.ToString();//��ݼ�
            ahk.SetVar("hotKeyHC", hotKeyHC);
            if (isAutoHC)
            {
                ahk.ExecFunction("AutoHC", hotKeyHC, isAutoHC.ToString());
                lastHotKey_HC = comboBox5.SelectedItem.ToString();
            }
            else
            {
                ahk.ExecFunction("AutoHC", hotKeyHC, isAutoHC.ToString());
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            SetAJ();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            SetAJ();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            SetAJ();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            SetAJ();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            SetAJ();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isAutoHC = checkBox10.Checked;   //�Ƿ�����
            var hotKeyHC = comboBox5.SelectedItem.ToString();   //�ȼ�
            double xp = 0;
            double yp = 0;
            if (resolutionRatio.SelectedIndex == 0)
            {
                xp = 1220;
                yp = 810;
            }
            else
            {
                xp = 1320;
                yp = 880;
            }
            ahk.SetVar("hotKeyHC", hotKeyHC);
            ahk.SetVar("xp", xp.ToString());
            ahk.SetVar("yp", yp.ToString());
            if (isAutoHC)
            {
                //ע���ϴ��ȼ�
                ahk.ExecFunction("AutoHC", lastHotKey_HC.ToString(), "False","0","0");

                ahk.ExecFunction("AutoHC", hotKeyHC, isAutoHC.ToString(), xp.ToString(), yp.ToString());
                lastHotKey_HC = hotKeyHC;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ahk.ExecFunction("QuickMap");
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            var isStartUp = checkBox1.Checked;
            if (isStartUp)
            {
                var jgTime = comboBox6.SelectedItem.ToString();  //��Ѫ����ͣ����ʱ��
                ahk.SetVar("jgTime", jgTime);
            }
        }

        private void checkBox4_CheckedChanged_1(object sender, EventArgs e)
        {
            AutoSkills();
        }

        private void resolutionRatio_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetScale();
        }
        //�ֱ��ʱ仯

    }
}