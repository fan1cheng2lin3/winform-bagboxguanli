using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BoxSystemMange.脚本类
{
    public class AutoAdaptWindowsSize
    {
        private double formOriginalWidth; // 窗体原始宽度
        private double formOriginalHeight; // 窗体原始高度
        private double scaleX; // 水平缩放比例
        private double scaleY; // 垂直缩放比例
        private readonly Dictionary<string, (double centerX, double centerY, double width, double height, float fontSize)> controlsInfo
            = new Dictionary<string, (double, double, double, double, float)>(); // 控件信息字典
        private readonly Form _form;
        private readonly Panel Win_Panel1 = new Panel();

        private const int MinWidth = 1000; // 最小宽度
        private const int MinHeight = 700; // 最小高度

        // 控件白名单，仅调整这些控件的大小
        private readonly HashSet<string> resizeControls = new HashSet<string> { "panel7", "panel2", "panel5", "label1", "label2", "button7" , "button11", "button6" ,
            "textBox1" , "button12" , "panel1", "panel4" , "label3", "label4", "label5" ,"button8","pictureBox1","flowLayoutPanel5","flowLayoutPanel3","flowLayoutPanel1",
        "zhuyepanel","panel12","panel6","panel3","panel9","panel10","flowLayoutPanel6","button1","button2","button3","button6","button13","button9","button10","button14","button16","button17",
        "button18"};//

        public AutoAdaptWindowsSize(Form form)
        {
            _form = form;
            // 添加panel1至窗体
            _form.Controls.Add(Win_Panel1);
            Win_Panel1.BorderStyle = BorderStyle.None; // 容器border样式
            Win_Panel1.Dock = DockStyle.Fill; // 设置填充
            Win_Panel1.BackColor = Color.Transparent; // 背景颜色设置为透明

            // 将窗体所有控件添加至panel1
            MoveControlsToPanel();

            // 保存窗体和控件初始大小
            InitControlsInfo(Win_Panel1);

            // 设置窗体的最小尺寸
            _form.MinimumSize = new Size(MinWidth, MinHeight);

            // 添加Resize事件处理程序
            _form.Resize += Form_ResizeEnd;
        }

        private void MoveControlsToPanel()
        {
            var controlsToMove = _form.Controls.Cast<Control>().Where(c => !string.IsNullOrWhiteSpace(c.Name) && c.Name != Win_Panel1.Name).ToList();
            foreach (var control in controlsToMove)
            {
                Win_Panel1.Controls.Add(control);
            }
        }

        public void InitControlsInfo(Control ctrlContainer)
        {
            if (ctrlContainer.Parent == _form) // 获取窗体的高度和宽度
            {
                formOriginalWidth = ctrlContainer.Width;
                formOriginalHeight = ctrlContainer.Height;
            }

            foreach (Control item in ctrlContainer.Controls)
            {
                if (!string.IsNullOrWhiteSpace(item.Name) && resizeControls.Contains(item.Name))
                {
                    // 添加信息：控件名 -> (中心X, 中心Y, 宽度, 高度, 字体大小)
                    controlsInfo[item.Name] = (item.Left + item.Width / 2.0,
                                               item.Top + item.Height / 2.0,
                                               item.Width,
                                               item.Height,
                                               item.Font.Size);
                }

                if (!(item is UserControl) && item.Controls.Count > 0)
                {
                    InitControlsInfo(item);
                }
            }
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            // 窗口调整结束后，进行控件调整
            FormSizeChanged();
        }

        public void FormSizeChanged()
        {
            if (controlsInfo.Count > 0) // 如果字典中有数据，即窗体改变
            {
                ControlsZoomScale(Win_Panel1); // 计算缩放比例
                ControlsChange(Win_Panel1); // 改变控件大小
            }
        }

        private void ControlsZoomScale(Control ctrlContainer)
        {
            scaleX = ctrlContainer.Width / formOriginalWidth;
            scaleY = ctrlContainer.Height / formOriginalHeight;
        }

        private void ControlsChange(Control ctrlContainer)
        {
            foreach (Control item in ctrlContainer.Controls)
            {
                if (!string.IsNullOrWhiteSpace(item.Name) && resizeControls.Contains(item.Name))
                {
                    if (!(item is UserControl) && item.Controls.Count > 0)
                    {
                        ControlsChange(item);
                    }

                    var (centerX, centerY, width, height, fontSize) = controlsInfo[item.Name];

                    double newWidth = width * scaleX;
                    double newHeight = height * scaleY;
                    item.Left = (int)(centerX * scaleX - newWidth / 2);
                    item.Top = (int)(centerY * scaleY - newHeight / 2);
                    item.Width = (int)newWidth;
                    item.Height = (int)newHeight;

                    float newFontSize = (float)(fontSize * Math.Min(scaleX, scaleY));
                    if (newFontSize > 0)
                    {
                        item.Font = new Font(item.Font.Name, newFontSize);
                    }
                }
            }
        }

        public (double scaleX, double scaleY) UpdateScaleAndGetCurrent()
        {
            ControlsZoomScale(Win_Panel1);
            return (scaleX, scaleY);
        }
    }
}
