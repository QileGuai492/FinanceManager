using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FinanceManager.Helpers
{
    /// <summary>
    /// 界面美化工具类，提供统一的配色常量、控件样式方法和圆角效果
    /// </summary>
    public static class UiHelper
    {
        // ===== 配色常量（主题色板）=====

        /// <summary>深蓝主色 #2C3E80 — 导航栏背景、主按钮</summary>
        public static readonly Color DeepBlue = Color.FromArgb(0x2C, 0x3E, 0x80);
        /// <summary>浅蓝辅色 #3F51B5 — 按钮悬停态、选中态</summary>
        public static readonly Color LightBlue = Color.FromArgb(0x3F, 0x51, 0xB5);
        /// <summary>极淡天蓝 #E3F2FD — 渐变起始色，柔和面板背景</summary>
        public static readonly Color SoftBlue = Color.FromArgb(0xE3, 0xF2, 0xFD);
        /// <summary>深蓝激活色 #1A237E — 当前选中导航按钮</summary>
        public static readonly Color ActiveBlue = Color.FromArgb(0x1A, 0x23, 0x7E);
        /// <summary>成功绿 #4CAF50 — 收入金额、正常状态</summary>
        public static readonly Color SuccessGreen = Color.FromArgb(0x4C, 0xAF, 0x50);
        /// <summary>警告橙 #FF9800 — 预算预警</summary>
        public static readonly Color WarningOrange = Color.FromArgb(0xFF, 0x98, 0x00);
        /// <summary>危险红 #F44336 — 支出金额、超支状态</summary>
        public static readonly Color DangerRed = Color.FromArgb(0xF4, 0x43, 0x36);
        /// <summary>背景浅灰 #F5F6FA — 窗体/内容区底色</summary>
        public static readonly Color BgLight = Color.FromArgb(0xF5, 0xF6, 0xFA);
        /// <summary>卡片白 #FFFFFF — 卡片、面板背景</summary>
        public static readonly Color CardWhite = Color.White;
        /// <summary>正文黑 #333333 — 标题、正文文字</summary>
        public static readonly Color TextDark = Color.FromArgb(0x33, 0x33, 0x33);
        /// <summary>次要灰 #888888 — 辅助文字、提示信息</summary>
        public static readonly Color TextGray = Color.FromArgb(0x88, 0x88, 0x88);
        /// <summary>边框灰 #E0E0E0 — 分割线、边框</summary>
        public static readonly Color BorderGray = Color.FromArgb(0xE0, 0xE0, 0xE0);

        // ===== 扩展色板 =====

        /// <summary>余额蓝 #2196F3 — 结余正值</summary>
        public static readonly Color BalanceBlue = Color.FromArgb(0x21, 0x96, 0xF3);
        /// <summary>成功绿悬停 #66BB6A</summary>
        public static readonly Color SuccessGreenHover = Color.FromArgb(0x66, 0xBB, 0x6A);
        /// <summary>危险红悬停 #EF5350</summary>
        public static readonly Color DangerRedHover = Color.FromArgb(0xEF, 0x53, 0x50);
        /// <summary>导航激活边框 #0D1555</summary>
        public static readonly Color NavBorderActive = Color.FromArgb(0x0D, 0x15, 0x55);
        /// <summary>导航普通边框 #3F51B5（即LightBlue）</summary>
        public static readonly Color NavBorderNormal = LightBlue;
        /// <summary>退出按钮文字色 #FFCDD2（浅粉）</summary>
        public static readonly Color LogoutPink = Color.FromArgb(0xFF, 0xCD, 0xD2);
        /// <summary>选中行背景 #E8EAF6（浅紫蓝）</summary>
        public static readonly Color SelectionBg = Color.FromArgb(0xE8, 0xEA, 0xF6);
        /// <summary>已支出正常色 #212121</summary>
        public static readonly Color SpentNormal = Color.FromArgb(0x21, 0x21, 0x21);
        /// <summary>超支行背景 #FFEBEE（浅红）</summary>
        public static readonly Color DangerBg = Color.FromArgb(0xFF, 0xEB, 0xEE);
        /// <summary>警告行背景 #FFF8E1（浅黄）</summary>
        public static readonly Color WarningBg = Color.FromArgb(0xFF, 0xF8, 0xE1);

        // 存储每个按钮的"有效背景色"，供 Paint 事件读取
        private static readonly Dictionary<Button, Color> _btnColors = new Dictionary<Button, Color>();

        // ===== 按钮样式 =====

        /// <summary>
        /// 将按钮设置为扁平风格 + 6px 抗锯齿圆角
        /// </summary>
        public static void StyleButton(Button btn, Color backColor, Color foreColor, int height = 36)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = foreColor;
            btn.UseVisualStyleBackColor = false;
            btn.Font = new Font("微软雅黑", 10f);
            btn.Height = height;
            btn.Cursor = Cursors.Hand;

            SetBtnColor(btn, backColor);

            btn.Paint += (s, e) =>
            {
                var c = GetBtnColor(btn);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);
                using (var path = GetRoundRect(rect, 6))
                using (var brush = new SolidBrush(c))
                    e.Graphics.FillPath(brush, path);
                // Transparent背景时Button不绘文字，手动绘制
                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, btn.ClientRectangle,
                    btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
        }

        /// <summary>更新按钮的绘制颜色并触发重绘</summary>
        private static void SetBtnColor(Button btn, Color c)
        {
            _btnColors[btn] = c;
            btn.Invalidate();
        }

        /// <summary>获取按钮当前绘制颜色</summary>
        private static Color GetBtnColor(Button btn)
        {
            return _btnColors.TryGetValue(btn, out var c) ? c : btn.BackColor;
        }

        /// <summary>
        /// 为按钮绑定鼠标悬停变色效果
        /// </summary>
        public static void BindHover(Button btn, Color normalColor, Color hoverColor)
        {
            SetBtnColor(btn, normalColor);
            btn.MouseEnter += (s, e) => SetBtnColor(btn, hoverColor);
            btn.MouseLeave += (s, e) => SetBtnColor(btn, normalColor);
        }

        // ===== 面板圆角 =====

        /// <summary>
        /// 为 Panel 添加圆角背景绘制效果（通过 Paint 事件实现）
        /// </summary>
        /// <param name="panel">目标面板</param>
        /// <param name="radius">圆角半径（像素）</param>
        /// <param name="backColor">背景填充色</param>
        public static void MakeRound(Panel panel, int radius, Color backColor)
        {
            panel.Paint += (s, e) =>
            {
                // 绘制圆角矩形区域
                var rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
                using (var path = GetRoundRect(rect, radius))
                using (var brush = new SolidBrush(backColor))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // 抗锯齿
                    e.Graphics.FillPath(brush, path);
                }
            };
        }

        // ===== DataGridView 统一美化 =====

        /// <summary>
        /// 对 DataGridView 应用统一的现代化表格样式：
        /// 白色背景、隔行变色、扁平表头、无行号列
        /// </summary>
        /// <param name="grid">目标表格控件</param>
        public static void StyleDataGridView(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = BorderGray;
            grid.RowHeadersVisible = false;              // 隐藏左侧行号
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.ReadOnly = true;                        // 禁止单击直接编辑，统一走编辑器面板
            grid.AllowUserToResizeRows = false;
            grid.EnableHeadersVisualStyles = false;      // 必须关闭才能自定义表头样式
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 表头样式：浅灰背景 + 深色粗体字
            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = BgLight,
                ForeColor = TextDark,
                Font = new Font("微软雅黑", 9f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(8, 0, 0, 0)
            };
            grid.ColumnHeadersHeight = 36;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // 默认行样式：白底黑字 + 浅蓝选中态
            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = TextDark,
                Font = new Font("微软雅黑", 9f),
                SelectionBackColor = SelectionBg,
                SelectionForeColor = TextDark,
                Padding = new Padding(8, 0, 0, 0)
            };
            grid.RowTemplate.Height = 32;

            // 隔行变色：偶数行浅灰底
            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(0xFA, 0xFA, 0xFA),
                ForeColor = TextDark,
                Font = new Font("微软雅黑", 9f),
                SelectionBackColor = SelectionBg,
                SelectionForeColor = TextDark,
                Padding = new Padding(8, 0, 0, 0)
            };
        }

        /// <summary>为 Panel 添加白→深蓝纵向渐变背景</summary>
        public static void MakeGradient(Control control, Color fromColor, Color toColor, float angle = 90f)
        {
            control.Paint += (s, e) =>
            {
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    control.ClientRectangle, fromColor, toColor, angle))
                {
                    e.Graphics.FillRectangle(brush, control.ClientRectangle);
                }
            };
        }

        // ===== TextBox 样式 =====

        /// <summary>统一 TextBox 高度36px、微软雅黑10pt、细边框</summary>
        public static void StyleTextBox(TextBox textBox)
        {
            textBox.Height = 36;
            textBox.Font = new Font("微软雅黑", 10f);
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        // ===== TextBox Placeholder =====

        /// <summary>为 TextBox 设置占位提示文字（失焦显示，聚焦清除）</summary>
        public static void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = TextGray;
            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholder) { textBox.Clear(); textBox.ForeColor = TextDark; }
            };
            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text)) { textBox.Text = placeholder; textBox.ForeColor = TextGray; }
            };
        }

        /// <summary>
        /// 生成圆角矩形 GraphicsPath（内部辅助方法）
        /// </summary>
        private static GraphicsPath GetRoundRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;  // 圆角直径
            // 四个角依次画弧：左上 → 右上 → 右下 → 左下
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.X + rect.Width - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.X + rect.Width - d, rect.Y + rect.Height - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
