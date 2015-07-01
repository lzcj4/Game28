using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Game28.UC
{
    public partial class UCNum28 : UserControl
    {
        public UCNum28()
        {
            InitializeComponent();
            this.Load += (sender, e) =>
            {
                LoadAllTextBox();
            };
        }

        const int rangeMin = 0, rangeMax = 27;
        IDictionary<int, Tuple<TextBox, int>> allTextBoxs = new Dictionary<int, Tuple<TextBox, int>>();

        private void LoadAllTextBox()
        {
            allTextBoxs[0] = new Tuple<TextBox, int>(txt0, 1);
            allTextBoxs[1] = new Tuple<TextBox, int>(txt1, 3);
            allTextBoxs[2] = new Tuple<TextBox, int>(txt2, 6);
            allTextBoxs[3] = new Tuple<TextBox, int>(txt3, 10);
            allTextBoxs[4] = new Tuple<TextBox, int>(txt4, 15);
            allTextBoxs[5] = new Tuple<TextBox, int>(txt5, 21);
            allTextBoxs[6] = new Tuple<TextBox, int>(txt6, 28);
            allTextBoxs[7] = new Tuple<TextBox, int>(txt7, 36);
            allTextBoxs[8] = new Tuple<TextBox, int>(txt8, 45);
            allTextBoxs[9] = new Tuple<TextBox, int>(txt9, 55);
            allTextBoxs[10] = new Tuple<TextBox, int>(txt10, 63);
            allTextBoxs[11] = new Tuple<TextBox, int>(txt11, 69);
            allTextBoxs[12] = new Tuple<TextBox, int>(txt12, 73);
            allTextBoxs[13] = new Tuple<TextBox, int>(txt13, 75);
            allTextBoxs[14] = new Tuple<TextBox, int>(txt14, 75);
            allTextBoxs[15] = new Tuple<TextBox, int>(txt15, 73);
            allTextBoxs[16] = new Tuple<TextBox, int>(txt16, 69);
            allTextBoxs[17] = new Tuple<TextBox, int>(txt17, 63);
            allTextBoxs[18] = new Tuple<TextBox, int>(txt18, 55);
            allTextBoxs[19] = new Tuple<TextBox, int>(txt19, 45);
            allTextBoxs[20] = new Tuple<TextBox, int>(txt20, 36);
            allTextBoxs[21] = new Tuple<TextBox, int>(txt21, 28);
            allTextBoxs[22] = new Tuple<TextBox, int>(txt22, 21);
            allTextBoxs[23] = new Tuple<TextBox, int>(txt23, 15);
            allTextBoxs[24] = new Tuple<TextBox, int>(txt24, 10);
            allTextBoxs[25] = new Tuple<TextBox, int>(txt25, 6);
            allTextBoxs[26] = new Tuple<TextBox, int>(txt26, 3);
            allTextBoxs[27] = new Tuple<TextBox, int>(txt27, 1);
        }

        public int[] GetValues()
        {
            int[] values = new int[allTextBoxs.Count];
            bool isNewValue = false;
            foreach (var key in allTextBoxs.Keys)
            {
                string num = allTextBoxs[key].Item1.Text;
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
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != 0)
                    allTextBoxs[i].Item1.Text = values[i].ToString();
            }
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
            foreach (var item in allTextBoxs.Values)
            {
                item.Item1.Text = "";
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

        /// <summary>
        /// Set textbox value by range, include the min and max value
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        private void SetRange(int min = rangeMin, int max = rangeMax)
        {
            ClearAllTextBox();
            int totalSum = 0;
            foreach (var key in allTextBoxs.Keys)
            {
                if (key >= min && key <= max)
                {
                    var tuple = allTextBoxs[key];
                    totalSum += tuple.Item2;
                    tuple.Item1.Text = tuple.Item2.ToString();
                }
            }
            lblTotal.Text = totalSum.ToString("N1");
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
            foreach (var item in allTextBoxs.Values)
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

            lblTotal.Text = totalSum.ToString("N1");
        }

        private void Texbox_TextChanged(object sender, EventArgs e)
        {
            if (isTextboxClicked)
            {
                int totalSum = 0;
                foreach (var item in allTextBoxs.Values)
                {
                    string itemText = item.Item1.Text;
                    if (string.IsNullOrEmpty(itemText))
                    {
                        continue;
                    }

                    int itemValue = 0;
                    if (int.TryParse(itemText, out itemValue))
                    {
                        totalSum += itemValue;
                    }
                }

                lblTotal.Text = totalSum.ToString("N1");
            }
        }

        bool isTextboxClicked = false;
        private void Texbox_MouseClick(object sender, MouseEventArgs e)
        {
            isTextboxClicked = true;
        }

        private void Textbox_Leave(object sender, EventArgs e)
        {
            isTextboxClicked = false;
        }
        #endregion

        #region  Rules events

        private void btnRuleAdd_Click(object sender, EventArgs e)
        {
            ClearAllTextBox();
            this.txtRule.Text = string.Empty;
            this.txtRule.Enabled = true;
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
            listRules.DataSource = rules;
        }

        private void btnRuleDel_Click(object sender, EventArgs e)
        {
            if (listRules.SelectedItem == null || !(listRules.SelectedItem is Rule))
            {
                return;
            }

            Rule rule = listRules.SelectedItem as Rule;
            rules.Remove(rule);
            AppSetting.SaveRules(rules);
            AppSetting.Save();
            LoadRules();
        }

        private void listRules_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listRules.SelectedItem == null || !(listRules.SelectedItem is Rule))
            {
                return;
            }

            ClearAllTextBox();
            Rule rule = listRules.SelectedItem as Rule;
            int totalSum = 0;
            for (int i = 0; i < rule.Values.Length; i++)
            {
                if (rule.Values[i] != 0)
                {
                    totalSum += rule.Values[i];
                    allTextBoxs[i].Item1.Text = rule.Values[i].ToString();
                }
            }

            lblTotal.Text = totalSum.ToString("N1");
        }


        #endregion

    }
}
