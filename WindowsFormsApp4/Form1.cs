using System;
using CCWin;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp4
{
    public partial class 超级记事本 : CCSkinMain
    {
        public 超级记事本()
        {
            InitializeComponent();
        }
        //设置数组保存下来菜单上要显示的字号的大小  
        public string[] FontSizeName = { "6", "7", "8", "9", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "32", "40", "44", "48", "56", "64", "72", "84", "100" };
        //利用榜和字号的关系定义一个字体大小的数组 
        public float[] FontSize = { 6, 7, 8, 9, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 32, 36, 40, 44, 48, 56, 64, 72, 84, 100 };

        private void Form1_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "正在执行：文件读写操作    ";

            this.toolStripStatusLabel2.Text = "当前文档字数合计：" + this.skinChatRichTextBox1.Text.Length;

            foreach (FontFamily font in FontFamily.Families)      //窗体打开时加载系统安装字体名称并输入到ComboBox控件中去  

            {
                this.skinComboBox1.Items.Add(font.Name);           //加载  
            }
            this.skinComboBox1.SelectedItem = "Consolas";
            foreach (string name in FontSizeName)                //窗体打开的时候将字号数组中的内容加载到ComboBox控件中  
            {
                this.skinComboBox2.Items.Add(name);
            }
            this.skinComboBox2.SelectedItem = "16";               //默认的字号为10  

        }
        private void skinLabel1_Click(object sender, EventArgs e)//关闭
        {
            if (this.skinChatRichTextBox1.Modified == true)
            {
                DialogResult r = MessageBox.Show("文件“" + this.Text + "”的内容已经修改。\n是否要保存?", "超级记事本(V2.0 Beta)提示！", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (r == DialogResult.Yes)
                {
                    另存为ToolStripMenuItem_Click(sender, e);
                    Application.Exit();
                }
                else if (r == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }
        private void 超级记事本_KeyUp(object sender, KeyEventArgs e)
        {
            this.toolStripStatusLabel2.Text = "当前文档字数合计：" + this.skinChatRichTextBox1.Text.Length;
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.skinChatRichTextBox1.Modified == true)
            {
                DialogResult r =
                    MessageBox.Show("文件 “" + this.Text + "” 的内容已经修改。\n是否要保存?", "记事本(V1.0.0.1 Beta)", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (r == DialogResult.Yes)
                {
                    另存为ToolStripMenuItem_Click(sender, e);
                    this.skinChatRichTextBox1.Clear();
                    this.Text = "";
                }
                else if (r == DialogResult.No)
                {
                    this.skinChatRichTextBox1.Clear();
                    this.Text = "";
                }
                else
                    return;
            }
            else
            {
                this.skinChatRichTextBox1.Clear();
            }
        }
        private void 打开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.skinChatRichTextBox1.Modified == true)
            {
                DialogResult r =
                    MessageBox.Show("文件 “" + this.Text + "” 的内容已经修改。\n是否要保存?", "记事本(V1.0.0.1 Beta)", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (r == DialogResult.Yes)
                {
                    保存ToolStripMenuItem_Click(sender, e);
                    OpenFile();
                }
                else if (r == DialogResult.No)
                {
                    OpenFile();
                }
                else
                    return;
            }
            else
            {
                OpenFile();
            }
            
        }
        private void OpenFile()
        {
            this.openFileDialog1.Title = "打开文件...";
            this.openFileDialog1.Filter = "文本文件(*.txt;*.rtf)|*.txt;*.rtf|所有文件(*.*)|*.*";
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.InitialDirectory = "桌面";
            this.openFileDialog1.ShowReadOnly = true;
            this.openFileDialog1.ReadOnlyChecked = false;
            this.openFileDialog1.FileName = "";

            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Text = this.openFileDialog1.FileName;

                StreamReader sr = new StreamReader
                    (this.openFileDialog1.FileName, Encoding.UTF8);
                this.skinChatRichTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (this.openFileDialog1.FileName == "")//没有打开
            {
                if (this.saveFileDialog1.FileName == "")//没有保存
                {
                    //弹出消息提示
                    if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter sw = new StreamWriter(this.saveFileDialog1.FileName);
                        sw.Write(this.skinChatRichTextBox1.Text);
                        sw.Close();
                    }
                }
                else//保存时
                {
                    //不弹出消息提示
                    StreamWriter sw = new StreamWriter(this.saveFileDialog1.FileName);
                    sw.Write(this.skinChatRichTextBox1.Text);
                    sw.Close();
                }
            }
            else//打开文件时
            {
                if (this.saveFileDialog1.FileName == "")//没有保存时
                {
                    StreamWriter sw = new StreamWriter(this.openFileDialog1.FileName);
                    sw.Write(this.skinChatRichTextBox1.Text);
                    sw.Close();
                }
                else
                {
                    StreamWriter sw = new StreamWriter(this.saveFileDialog1.FileName);
                    sw.Write(this.skinChatRichTextBox1.Text);
                    sw.Close();
                }
            }

        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Title = "另存为...";
            this.saveFileDialog1.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            this.saveFileDialog1.InitialDirectory = "桌面";
            this.saveFileDialog1.FileName = "";
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter
                    (this.saveFileDialog1.FileName);
                sw.Write(this.skinChatRichTextBox1.Text);
                sw.Close();
            }

        }
        private void 页面设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog1.Document = this.printDocument1;
            this.pageSetupDialog1.ShowDialog();
        }
        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.printDialog1.AllowPrintToFile = true;
            this.printDialog1.Document = this.printDocument1;
            if (this.printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.skinChatRichTextBox1.Modified == true)
            {
                DialogResult r = MessageBox.Show("文件“" + this.Text + "”的内容已经修改。\n是否要保存?", "超级记事本(V2.0 Beta)提示！", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                if (r == DialogResult.Yes)
                {
                    另存为ToolStripMenuItem_Click(sender, e);
                    Application.Exit();
                }
                else if (r == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }
        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            skinChatRichTextBox1.Undo();
        }
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (skinChatRichTextBox1.SelectedText == "")
            {
                return;
            }
            else
            {
                skinChatRichTextBox1.Cut();
            }
        }
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (skinChatRichTextBox1.SelectedText == "")
            {
                return;
            }
            else
            {
                skinChatRichTextBox1.Copy();
            }
        }
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            skinChatRichTextBox1.Paste();
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (skinChatRichTextBox1.SelectedText != "")
                skinChatRichTextBox1.SelectedText = "";
        }
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            skinChatRichTextBox1.SelectAll();
        }
        private void AllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            skinTextBox1.Focus();
        }
        private void 日期时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            skinChatRichTextBox1.SelectedText = DateTime.Now.ToString();
        }
        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.自动换行ToolStripMenuItem.Checked == false)
            {
                this.自动换行ToolStripMenuItem.Checked = true;
                this.skinChatRichTextBox1.WordWrap = true;
            }
            else
            {
                this.自动换行ToolStripMenuItem.Checked = false;
                this.skinChatRichTextBox1.WordWrap = false;
            }
        }
        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.fontDialog1.ShowEffects = true;
            this.fontDialog1.Font = this.skinChatRichTextBox1.SelectionFont;
            if (this.fontDialog1.ShowDialog() == DialogResult.OK)
            {
                this.skinChatRichTextBox1.SelectionFont = this.fontDialog1.Font;
            }
        }
        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.Color = this.skinChatRichTextBox1.SelectionColor;
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.skinChatRichTextBox1.SelectionColor = this.colorDialog1.Color;
            }
        }
        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)


        {
            if (this.状态栏ToolStripMenuItem.Checked == false)
            {
                this.状态栏ToolStripMenuItem.Checked = true;
                this.statusStrip1.Visible = true;
                this.skinChatRichTextBox1.Height -= 22;
            }
            else
            {
                this.状态栏ToolStripMenuItem.Checked = false;
                this.statusStrip1.Visible = false;
                this.skinChatRichTextBox1.Height += 22;
            }
        }


        private void 左toolStripButton_Click(object sender, EventArgs e)
        {
            this.skinChatRichTextBox1.SelectedText = this.skinChatRichTextBox1.SelectedText.Trim();
            this.skinChatRichTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }
        private void 居中toolStripButton_Click(object sender, EventArgs e)
        {
            this.skinChatRichTextBox1.SelectedText = this.skinChatRichTextBox1.SelectedText.Trim();
            this.skinChatRichTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }
        private void 右toolStripButton_Click(object sender, EventArgs e)
        {
            this.skinChatRichTextBox1.SelectedText = this.skinChatRichTextBox1.SelectedText.Trim();
            this.skinChatRichTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }


        private void 加粗toolStripButton_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Bold);
            skinChatRichTextBox1.Focus();
        }
        private void 倾斜toolStripButton_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Italic);
            skinChatRichTextBox1.Focus();
        }
        private void 下划线toolStripButton_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Underline);
            skinChatRichTextBox1.Focus();
        }
        //下划线 加粗 倾斜 改变方法
        private void ChangeFontStyle(FontStyle style)
        {
            if (style != FontStyle.Bold && style != FontStyle.Italic &&
                style != FontStyle.Underline)
                throw new System.InvalidProgramException("字体格式错误");
            RichTextBox tempRichTextBox = new RichTextBox();  //将要存放被选中文本的副本  
            int curRtbStart = skinChatRichTextBox1.SelectionStart;
            int len = skinChatRichTextBox1.SelectionLength;
            int tempRtbStart = 0;
            Font font = skinChatRichTextBox1.SelectionFont;
            if (len <= 1 && font != null) //与上边的那段代码类似，功能相同  
            {
                if (style == FontStyle.Bold && font.Bold ||
                    style == FontStyle.Italic && font.Italic ||
                    style == FontStyle.Underline && font.Underline)
                {
                    skinChatRichTextBox1.SelectionFont = new Font(font, font.Style ^ style);
                }
                else if (style == FontStyle.Bold && !font.Bold ||
                         style == FontStyle.Italic && !font.Italic ||
                         style == FontStyle.Underline && !font.Underline)
                {
                    skinChatRichTextBox1.SelectionFont = new Font(font, font.Style | style);
                }
                return;
            }
            tempRichTextBox.Rtf = skinChatRichTextBox1.SelectedRtf;
            tempRichTextBox.Select(len - 1, 1); //选中副本中的最后一个文字  
                                                //克隆被选中的文字Font，这个tempFont主要是用来判断  
                                                //最终被选中的文字是否要加粗、去粗、斜体、去斜、下划线、去下划线  
            Font tempFont = (Font)tempRichTextBox.SelectionFont.Clone();

            //清空2和3  
            for (int i = 0; i < len; i++)
            {
                tempRichTextBox.Select(tempRtbStart + i, 1);  //每次选中一个，逐个进行加粗或去粗  
                if (style == FontStyle.Bold && tempFont.Bold ||
                    style == FontStyle.Italic && tempFont.Italic ||
                    style == FontStyle.Underline && tempFont.Underline)
                {
                    tempRichTextBox.SelectionFont =
                        new Font(tempRichTextBox.SelectionFont,
                                 tempRichTextBox.SelectionFont.Style ^ style);
                }
                else if (style == FontStyle.Bold && !tempFont.Bold ||
                         style == FontStyle.Italic && !tempFont.Italic ||
                         style == FontStyle.Underline && !tempFont.Underline)
                {
                    tempRichTextBox.SelectionFont =
                        new Font(tempRichTextBox.SelectionFont,
                                 tempRichTextBox.SelectionFont.Style | style);
                }
            }
            tempRichTextBox.Select(tempRtbStart, len);
            skinChatRichTextBox1.SelectedRtf = tempRichTextBox.SelectedRtf; //将设置格式后的副本拷贝给原型  
            skinChatRichTextBox1.Select(curRtbStart, len);
        }

        private void skinComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.skinChatRichTextBox1.SelectionFont = new Font(this.skinComboBox1.Text.Trim(), this.skinChatRichTextBox1.SelectionFont.Size, this.skinChatRichTextBox1.SelectionFont.Style);
                skinChatRichTextBox1.Focus();
            }
            catch (Exception)
            {
                SetFontName(this.skinComboBox1.Text.Trim());
            }
        }
        //不同字体改变同一种字体
        private void SetFontName(string fontName)
        {
            RichTextBox tempRichTextBox = new RichTextBox(); //将要存放被选中文本的副本
            int curRtbStart = skinChatRichTextBox1.SelectionStart;
            int len = skinChatRichTextBox1.SelectionLength;
            int tempRtbStart = 0;
            Font font = skinChatRichTextBox1.SelectionFont;
            if (len <= 1 && font != null)
            {
                skinChatRichTextBox1.SelectionFont = new Font(fontName, font.Size, font.Style);
                return;
            }
            tempRichTextBox.Rtf = skinChatRichTextBox1.SelectedRtf;
            for (int i = 0; i < len; i++)
            {
                tempRichTextBox.Select(tempRtbStart + i, 1); //每次选中一个，逐个设置字体大小
                tempRichTextBox.SelectionFont =
                new Font(fontName, tempRichTextBox.SelectionFont.Size,
                tempRichTextBox.SelectionFont.Style);
            }
            tempRichTextBox.Select(tempRtbStart, len);
            skinChatRichTextBox1.SelectedRtf = tempRichTextBox.SelectedRtf; //将设置字体大小后的副本拷贝给原型
            skinChatRichTextBox1.Select(curRtbStart, len);
            // 激活文本框
            skinChatRichTextBox1.Select();
        }


        private void skinComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.skinChatRichTextBox1.SelectionFont = new Font(this.skinChatRichTextBox1.SelectionFont.FontFamily, FontSize[this.skinComboBox2.SelectedIndex], this.skinChatRichTextBox1.SelectionFont.Style);
            skinChatRichTextBox1.Focus();
        }

        private void skinComboBox2_TextChanged(object sender, EventArgs e)
        {
            if (skinComboBox2.Text.Trim() != "")
            {
                try
                {
                    this.skinChatRichTextBox1.SelectionFont = new Font(this.skinChatRichTextBox1.SelectionFont.FontFamily, float.Parse(skinComboBox2.Text.Trim()), this.skinChatRichTextBox1.SelectionFont.Style);
                }
                catch (Exception)
                {
                    MessageBox.Show("别捣蛋。。。格式有误！！！");
                    return;
                }
            }

        }

        int start = 0;//查找的起始位置
        string str = "";//查找的内容
        string str2 = ""; //替换
        RichTextBoxFinds f;
        int i = 0;
        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void PictureBox2_Click(object sender, EventArgs e)
        {
            str = this.skinTextBox1.Text;//找的内容
            str2 = this.skinTextBox2.Text;
            start = skinChatRichTextBox1.Find(str, start, f);
            if (start == -1)
            {
                MessageBox.Show("对不起！查找不到与“" + str + "”内容相匹配的信息！", "(超级记事本 V2.0 Beta)查找提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                start = 0;
            }
            else
            {
                skinChatRichTextBox1.SelectedText = str2;
                start = start + str.Length;//找到后从找到位置之后开始下一次
                skinChatRichTextBox1.Focus(); //给予焦点
            }
        }
        private void PictureBox3_Click(object sender, EventArgs e)
        {
            str = this.skinTextBox1.Text;//找的内容
            str2 = this.skinTextBox2.Text;
            start = skinChatRichTextBox1.Find(str, start, f);
            while (start != -1)
            {
                skinChatRichTextBox1.SelectedText = str2;
                start = start + str.Length;
                start = skinChatRichTextBox1.Find(str, start, f);
                i++;
            }
            MessageBox.Show("全部替换完毕！全部一共替换了" + i.ToString() + "次", "(超级记事本 V2.0 Beta)替换完毕信息！");
            start = 0;
        }

        private void skinPictureBox1_MouseEnter(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this.skinPictureBox1, "查找下一个");
        }
        private void skinPictureBox2_MouseEnter(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this.skinPictureBox2, "替换");
        }
        private void skinPictureBox3_MouseEnter(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this.skinPictureBox3, "全部替换");
        }

        private void skinLabel2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }
    }
}

