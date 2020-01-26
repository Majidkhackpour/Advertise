using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer;
using FMessegeBox;

namespace Ads.Forms.Simcard
{
    public partial class frmSimcard_Main : Form
    {
        private SimcardBussines cls;
        public frmSimcard_Main()
        {
            InitializeComponent();
            cls = new SimcardBussines();
        }

        public frmSimcard_Main(Guid guid)
        {
            InitializeComponent();
            cls = SimcardBussines.GetAsync(guid);
        }

        private async Task FillComboBox()
        {
            try
            {
                var list = await SimcardBussines.GetAllAsync();
                var a = list.Select(q => q.Operator).Distinct().ToList();
                cmbOperator.DataSource = a;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        private async void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                btnFinish.Enabled = false;
                if (cls.Guid == Guid.Empty)
                {
                    cls.Guid = Guid.NewGuid();
                    cls.DateSabt = DateConvertor.M2SH(DateTime.Now);
                }
                if (string.IsNullOrWhiteSpace(txtNumber.Text))
                {
                    FarsiMessegeBox.Show("لطفا شماره را وارد نمایید");
                    txtNumber.Focus();
                    return;
                }
                if (txtNumber.Text.Length < 10 || txtNumber.Text.Length > 12)
                {
                    FarsiMessegeBox.Show("شماره وارد شده اشتباه است");
                    txtNumber.Focus();
                    return;
                }
                if (!SimcardBussines.Check_Number(long.Parse(txtNumber.Text), cls.Guid))
                {
                    FarsiMessegeBox.Show("شماره وارد شده تکراری است");
                    txtNumber.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtOwner.Text))
                {
                    FarsiMessegeBox.Show("لطفا مالک را وارد نمایید");
                    txtNumber.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbOperator.Text))
                {
                    FarsiMessegeBox.Show("لطفا اپراتور را وارد نمایید");
                    txtNumber.Focus();
                    return;
                }

                cls.OwnerName = txtOwner.Text;
                cls.Status = true;
                cls.Number = long.Parse(txtNumber.Text);
                cls.NextUseDivar = DateTime.Now;
                cls.NextUseSheypoor = DateTime.Now;
                cls.NextUseDivarChat = DateTime.Now;
                cls.Operator = cmbOperator.Text;
                cls.UserName = txtUserName.Text;
                await cls.SaveAsync();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
            finally
            {
                btnFinish.Enabled = true;
            }
        }

        private void txtNumber_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtNumber);
        }

        private void txtOwner_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtOwner);
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtUserName);
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtUserName);
        }

        private void txtOwner_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtOwner);
        }

        private void txtNumber_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtNumber);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmSimcard_Main_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        btnFinish.PerformClick();
                        break;
                    case Keys.Escape:
                        btnCancel.PerformClick();
                        break;
                }
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async void frmSimcard_Main_Load(object sender, EventArgs e)
        {
            await FillComboBox();
            Set_Data();
            var sim = await SimcardBussines.GetAllAsync();
            var _source = new AutoCompleteStringCollection();

            foreach (var item in sim)
            {
                _source.Add(item.OwnerName);
            }

            txtOwner.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtOwner.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtOwner.AutoCompleteCustomSource = _source;
        }
        private void Set_Data()
        {
            try
            {
                txtNumber.Text = cls.Number.ToString();
                txtOwner.Text = cls.OwnerName;
                cmbOperator.Text = cls.Operator;
                txtUserName.Text = cls.UserName;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
    }
}
