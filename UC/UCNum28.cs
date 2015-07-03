using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Game28.UC
{
    public partial class UCNum28 : UserControl
    {
        public UCNum28()
        {
            InitializeComponent();
            this.Load += (sender, e) =>
            {
                LoadAllUIMap();
                LoadRules();
            };
        }

        const int rangeMin = 0, rangeMax = 27;
        IDictionary<int, Tuple<TextBox, int, CheckBox>> allNumCtlDic = new Dictionary<int, Tuple<TextBox, int, CheckBox>>();

        private void LoadAllUIMap()
        {
            allNumCtlDic[0] = new Tuple<TextBox, int, CheckBox>(txt0, 1, chk0);
            allNumCtlDic[1] = new Tuple<TextBox, int, CheckBox>(txt1, 3, chk1);
            allNumCtlDic[2] = new Tuple<TextBox, int, CheckBox>(txt2, 6, chk2);
            allNumCtlDic[3] = new Tuple<TextBox, int, CheckBox>(txt3, 10, chk3);
            allNumCtlDic[4] = new Tuple<TextBox, int, CheckBox>(txt4, 15, chk4);
            allNumCtlDic[5] = new Tuple<TextBox, int, CheckBox>(txt5, 21, chk5);
            allNumCtlDic[6] = new Tuple<TextBox, int, CheckBox>(txt6, 28, chk6);
            allNumCtlDic[7] = new Tuple<TextBox, int, CheckBox>(txt7, 36, chk7);
            allNumCtlDic[8] = new Tuple<TextBox, int, CheckBox>(txt8, 45, chk8);
            allNumCtlDic[9] = new Tuple<TextBox, int, CheckBox>(txt9, 55, chk9);
            allNumCtlDic[10] = new Tuple<TextBox, int, CheckBox>(txt10, 63, chk10);
            allNumCtlDic[11] = new Tuple<TextBox, int, CheckBox>(txt11, 69, chk11);
            allNumCtlDic[12] = new Tuple<TextBox, int, CheckBox>(txt12, 73, chk12);
            allNumCtlDic[13] = new Tuple<TextBox, int, CheckBox>(txt13, 75, chk13);
            allNumCtlDic[14] = new Tuple<TextBox, int, CheckBox>(txt14, 75, chk14);
            allNumCtlDic[15] = new Tuple<TextBox, int, CheckBox>(txt15, 73, chk15);
            allNumCtlDic[16] = new Tuple<TextBox, int, CheckBox>(txt16, 69, chk16);
            allNumCtlDic[17] = new Tuple<TextBox, int, CheckBox>(txt17, 63, chk17);
            allNumCtlDic[18] = new Tuple<TextBox, int, CheckBox>(txt18, 55, chk18);
            allNumCtlDic[19] = new Tuple<TextBox, int, CheckBox>(txt19, 45, chk19);
            allNumCtlDic[20] = new Tuple<TextBox, int, CheckBox>(txt20, 36, chk20);
            allNumCtlDic[21] = new Tuple<TextBox, int, CheckBox>(txt21, 28, chk21);
            allNumCtlDic[22] = new Tuple<TextBox, int, CheckBox>(txt22, 21, chk22);
            allNumCtlDic[23] = new Tuple<TextBox, int, CheckBox>(txt23, 15, chk23);
            allNumCtlDic[24] = new Tuple<TextBox, int, CheckBox>(txt24, 10, chk24);
            allNumCtlDic[25] = new Tuple<TextBox, int, CheckBox>(txt25, 6, chk25);
            allNumCtlDic[26] = new Tuple<TextBox, int, CheckBox>(txt26, 3, chk26);
            allNumCtlDic[27] = new Tuple<TextBox, int, CheckBox>(txt27, 1, chk27);
        }

        public int[] GetValues()
        {
            int[] values = new int[allNumCtlDic.Count];
            bool isNewValue = false;
            foreach (var key in allNumCtlDic.Keys)
            {
                string num = allNumCtlDic[key].Item1.Text;
                if (!string.IsNullOrEmpty(num))
                {
                    int itemValue = 0;
                    if (int.TryParse(num, out  itemValue))
                    {
                        values[key] = itemValue;
                        isNewValue = true;
                    }
                }
            }
            if (!isNewValue)
            {
                MessageBox.Show("请加入有效的U豆来竞猜");
                return null;
            }
            return values;
        }

        public void SetValues(int[] values)
        {
            if (values == null || values.Length != Speed28.MaxLen)
            {
                throw new ArgumentException();
            }

            ClearAllTextBox();
            int totalSum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != 0)
                {
                    totalSum += values[i];

                    allNumCtlDic[i].Item1.Text = values[i].ToString();
                    allNumCtlDic[i].Item3.Checked = true;
                }
            }

            lblTotal.Text = totalSum.ToString("N0");
        }

        #region Num event

        private void btnClear_Click(object sender, EventArgs e)
        {
            SetRange(-1, -1);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            SetRange();
        }

        private void ClearAllTextBox()
        {
            foreach (var item in allNumCtlDic.Values)
            {
                item.Item1.Text = "";
                item.Item3.Checked = false;
            }
        }

        private void btnSmall_Click(object sender, EventArgs e)
        {
            SetRange(0, 13);
        }

        private void btnLarge_Click(object sender, EventArgs e)
        {
            SetRange(14);
        }

        private void btnMiddle_Click(object sender, EventArgs e)
        {
            SetRange(10, 17);
        }

        private void btnSmallEdge_Click(object sender, EventArgs e)
        {
            SetRange(0, 9);
        }

        private void btnLargeEdge_Click(object sender, EventArgs e)
        {
            SetRange(18);
        }

        private void btnOdd_Click(object sender, EventArgs e)
        {
            ClearAllTextBox();
            int totalSum = 0;
            foreach (var key in allNumCtlDic.Keys)
            {
                if (key % 2 == 1)
                {
                    var tuple = allNumCtlDic[key];
                    totalSum += tuple.Item2;
                    tuple.Item1.Text = tuple.Item2.ToString();
                    tuple.Item3.Checked = true;
                }
            }
            lblTotal.Text = totalSum.ToString("N0");
        }

        private void btnEven_Click(object sender, EventArgs e)
        {
            ClearAllTextBox();
            int totalSum = 0;
            foreach (var key in allNumCtlDic.Keys)
            {
                if (key % 2 == 0)
                {
                    var tuple = allNumCtlDic[key];
                    totalSum += tuple.Item2;
                    tuple.Item1.Text = tuple.Item2.ToString();
                    tuple.Item3.Checked = true;
                }
            }
            lblTotal.Text = totalSum.ToString("N0");
        }

        private void btnMei_Click(object sender, EventArgs e)
        {
            ClearAllTextBox();
            int totalSum = 0;
            foreach (var key in allNumCtlDic.Keys)
            {
                if (key != 13 && key != 14 && key != 15)
                {
                    var tuple = allNumCtlDic[key];
                    totalSum += tuple.Item2;
                    tuple.Item1.Text = tuple.Item2.ToString();
                    tuple.Item3.Checked = true;
                }
            }
            lblTotal.Text = totalSum.ToString("N0");
        }

        /// <summary>
        /// Set textbox value by range, include the min and max value
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        private void SetRange(int min = rangeMin, int max = rangeMax)
        {
            ClearAllTextBox();
            int totalSum = 0;
            foreach (var key in allNumCtlDic.Keys)
            {
                if (key >= min && key <= max)
                {
                    var tuple = allNumCtlDic[key];
                    totalSum += tuple.Item2;
                    tuple.Item1.Text = tuple.Item2.ToString();
                    tuple.Item3.Checked = true;
                }
            }
            lblTotal.Text = totalSum.ToString("N0");
        }

        private void btnRate_Click(object sender, EventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();
            double rate = 1;
            if (!double.TryParse(tag, out rate))
            {
                return;
            }

            int totalSum = 0;
            foreach (var item in allNumCtlDic.Values)
            {
                string itemText = item.Item1.Text;
                if (string.IsNullOrEmpty(itemText))
                {
                    continue;
                }

                int itemValue = 0;
                if (int.TryParse(itemText, out itemValue))
                {
                    int newValue = (int)Math.Ceiling(itemValue * rate);
                    totalSum += newValue;
                    item.Item1.Text = newValue.ToString();
                }
            }

            lblTotal.Text = totalSum.ToString("N0");
        }

        private void Textbox_KeyUp(object sender, KeyEventArgs e)
        {
            SumTotal();
        }

        private void SumTotal()
        {
            int totalSum = 0;
            foreach (var item in allNumCtlDic.Values)
            {
                string itemText = item.Item1.Text;
                if (string.IsNullOrEmpty(itemText))
                {
                    item.Item3.Checked = false;
                    continue;
                }

                item.Item3.Checked = true;
                int itemValue = 0;
                if (int.TryParse(itemText, out itemValue))
                {
                    totalSum += itemValue;
                }
            }

            lblTotal.Text = totalSum.ToString("N0");
        }

        private void Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            int index = int.Parse(chk.Name.Replace("chk", ""));
            Tuple<TextBox, int, CheckBox> tuple = allNumCtlDic[index];
            if (chk.Checked)
            {
                if (string.IsNullOrEmpty(tuple.Item1.Text))
                {
                    tuple.Item1.Text = tuple.Item2.ToString();
                    SumTotal();
                }
            }
            else
            {
                tuple.Item1.Text = "";
                   SumTotal();
            }
        }

        #endregion

        #region  Rules events

        private void btnRuleAdd_Click(object sender, EventArgs e)
        {
            ClearAllTextBox();
            this.txtRule.Text = string.Empty;
            this.txtRule.Enabled = true;
        }

        private class UIRule
        {
            public Rule Rule { get; private set; }
            public UIRule(Rule rule)
            {
                Rule = rule;
            }
            public override string ToString()
            {
                return Rule.Name;
            }
        }

        private void btnRuleSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtRule.Text))
            {
                MessageBox.Show("录入有效规则名");
                return;
            }

            int[] values = GetValues();
            if (values == null)
            {
                return;
            }

            Rule rule = new Rule(this.txtRule.Text, values);
            rules.Add(rule);
            AppSetting.SaveRules(rules);
            AppSetting.Save();

            LoadRules();
        }

        IList<Rule> rules = new List<Rule>();
        private void LoadRules()
        {
            rules = AppSetting.GetRules();
            listRules.DataSource = null;
            listRules.DataSource = rules.Select(item => new UIRule(item)).ToList();
        }

        private void btnRuleDel_Click(object sender, EventArgs e)
        {
            if (listRules.SelectedItem == null || !(listRules.SelectedItem is UIRule))
            {
                return;
            }

            UIRule rule = listRules.SelectedItem as UIRule;
            rules.Remove(rule.Rule);
            AppSetting.SaveRules(rules);
            AppSetting.Save();
            LoadRules();
        }

        private void listRules_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listRules.SelectedItem == null || !(listRules.SelectedItem is UIRule))
            {
                return;
            }

            ClearAllTextBox();
            Rule rule = (listRules.SelectedItem as UIRule).Rule;
            SetValues(rule.Values);
        }


        #endregion
    }
}
