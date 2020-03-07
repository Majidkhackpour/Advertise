using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer;
using DataLayer.Enums;
using ErrorHandler;
using FMessegeBox;

namespace Ads.Forms.Mains
{
    public partial class frmAds : Form
    {
        private AdvertiseBussines _adv;
        readonly List<string> lstList = new List<string>();
        private List<string> lstContent = new List<string>();
        private string _pictureNameForClick = null;
        private PictureBox _orGpicBox;
        private PictureBox _fakepicBox;
        private string _picNameJari = "";
        private int top;
        private Guid _groupGuid;
        public Guid GroupGuid
        {
            get => _groupGuid;
            set => _groupGuid = value;
        }
        private async Task LoadGroups()
        {
            try
            {
                var list = await AdvGroupBussines.GetAllAsync();
                list = list.Where(q => q.Status).OrderBy(q => q.Name).ToList();
                ParentBindingSource.DataSource = list;
                trvGroup.Nodes.Clear();
                var lst = await AdvGroupBussines.GetAllAsync();
                lst = lst.Where(q => q.Status && q.ParentGuid == Guid.Empty).OrderBy(q => q.Name).ToList();
                var node = new TreeNode { Text = "همه گروه ها", Name = Guid.Empty.ToString() };
                trvGroup.Nodes.Add(node);
                foreach (var item in lst)
                {
                    node = new TreeNode { Text = item.Name, Name = item.Guid.ToString() };
                    trvGroup.Nodes.Add(node);
                }
                lst = await AdvGroupBussines.GetAllAsync();
                lst = lst.Where(q => q.Status && q.ParentGuid != Guid.Empty).OrderBy(q => q.Name).ToList();
                foreach (var item in lst)
                {
                    foreach (TreeNode n in trvGroup.Nodes)
                    {
                        if (item.ParentGuid.ToString() == n.Name)
                        {
                            node = new TreeNode { Text = item.Name, Name = item.Guid.ToString() };
                            n.Nodes.Add(node);
                        }
                    }
                }

                var lstChild = await AdvertiseBussines.GetAllAsync();
                lstChild = lstChild.Where(q => q.Status).OrderBy(q => q.AdvName).ToList();
                foreach (var item in lstChild)
                {
                    foreach (TreeNode n in trvGroup.Nodes)
                    {
                        if (item.GroupGuid.ToString() == n.Name)
                        {
                            node = new TreeNode { Text = item.AdvName, Name = item.Guid.ToString() };
                            n.Nodes.Add(node);
                            continue;
                        }
                        foreach (TreeNode v in n.Nodes)
                        {
                            if (item.GroupGuid.ToString() == v.Name)
                            {
                                node = new TreeNode { Text = item.AdvName, Name = item.Guid.ToString() };
                                v.Nodes.Add(node);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        public frmAds()
        {
            InitializeComponent();
            _adv = new AdvertiseBussines();
            contextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            grpAccount.Enabled = false;
            btnInsImage.Enabled = false;
            btnDeleteImage.Enabled = false;
            btnFinish.Text = "تایید (F5)";
        }
        private async Task Set_Data()
        {
            await LoadGroups();
            await FillAdvGroups();
            txtDesc.Text = "";
            txtName.Text = _adv.AdvName;
            if (_adv.Content != null && _adv.Content.Count != 0)
                lstContent = _adv.Content.Select(q => q.Content).OrderBy(p => p).ToList();
            top = 0;
            if (lstContent.Count > 0)
                txtDesc.Text = lstContent[0];
            txtTitles.Text = "";
            if (_adv.Titles != null && _adv.Titles.Count != 0)
            {
                var sortedTitles = _adv.Titles.Select(q => q.Title).OrderBy(p => p).ToList();
                FillTxtTitles(sortedTitles);
            }

            txtPrice.Text = _adv.Price;
            fPanel.Controls.Clear();
            lstList.Clear();
            if (_adv.Images != null && _adv.Images.Count != 0)
                foreach (var image in _adv.Images)
                    lstList.Add(image.PathGuid);
            txtName.Focus();
            Make_Picture_Boxes(lstList);
            if (_adv.Guid != Guid.Empty)
            {
                cmbGroup.SelectedValue = _adv.GroupGuid;
                cmbDivarCat1.SelectedValue = _adv.DivarCatGuid1;
                cmbDivarCat2.SelectedValue = _adv.DivarCatGuid2;
                cmbDivarCat3.SelectedValue = _adv.DivarCatGuid3;
                cmbSheypoorCat1.SelectedValue = _adv.SheypoorCatGuid1;
                cmbSheypoorCat2.SelectedValue = _adv.SheypoorCatGuid2;
            }
        }

        private async Task FillAdvGroups()
        {
            try
            {
                var divarCat1 = await AdvCategoryBussines.GetAllAsync(Guid.Empty, AdvertiseType.Divar);
                divarCat1 = divarCat1.OrderBy(q => q.Name).ToList();
                DivarCat1BingingSource.DataSource = divarCat1;


                var SheypoorCat1 = await AdvCategoryBussines.GetAllAsync(Guid.Empty, AdvertiseType.Sheypoor);
                SheypoorCat1 = SheypoorCat1.OrderBy(q => q.Name).ToList();
                SheypoorCat1BingingSource.DataSource = SheypoorCat1;

            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        private async void mnuInsGroup_Click(object sender, System.EventArgs e)
        {
            try
            {
                var frm = new frmAdsGroup();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadGroups();
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void frmAds_Load(object sender, EventArgs e)
        {
            await Set_Data();
        }

        private async void mnuEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var group = await AdvGroupBussines.GetAsync(GroupGuid);
                if (group != null)
                {
                    //GroupEdit
                    var frm = new frmAdsGroup(GroupGuid);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        await LoadGroups();
                    }

                    return;
                }
                else
                {
                    //AdvEdit
                    var child = await AdvertiseBussines.GetAsync(GroupGuid);
                    if (child != null)
                    {
                        _adv = child;
                        await Set_Data();
                        grpAccount.Enabled = true;
                        btnInsImage.Enabled = true;
                        btnDeleteImage.Enabled = true;
                        btnFinish.Text = "ویرایش (F5)";
                        return;
                    }
                }
                WebErrorLog.ErrorInstence.StartErrorLog("گروه یا آگهی انتخابی معتبر نمی باشد",false);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void trvGroup_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                var node = trvGroup.SelectedNode;
                var group = AdvGroupBussines.Get(Guid.Parse(node.Name));
                if (group != null)
                    GroupGuid = group.Guid;
                else
                {
                    var ads = await AdvertiseBussines.GetAsync(Guid.Parse(node.Name));
                    if (ads != null)
                        GroupGuid = ads.Guid;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var group = await AdvGroupBussines.GetAsync(GroupGuid);
                if (group != null)
                {
                    var childCount = AdvGroupBussines.ChildCounter(group.Guid);
                    if (childCount > 0)
                    {
                        WebErrorLog.ErrorInstence.StartErrorLog(
                            $"گروه {group.Name} به علت داشتن {childCount} زیر گروه فعال مجاز به حذف نمی باشد", false);
                        return;
                    }
                    var message = "آیا از حذف گروه آگهی " + group.Name + " " + "اطمینان دارید؟";
                    if (FarsiMessegeBox.Show(message, "حذف گروه آگهی", FMessegeBoxButtons.YesNo, FMessegeBoxIcons.Question) == DialogResult.Yes)
                    {
                        group = AdvGroupBussines.Change_Status(GroupGuid, false);
                        await group.SaveAsync();
                        await LoadGroups();
                    }

                    return;
                }
                else
                {
                    //AdvDelete
                    var adv = await AdvertiseBussines.GetAsync(GroupGuid);
                    if (adv != null)
                    {
                        var message = "آیا از حذف آگهی " + adv.AdvName + " " + "اطمینان دارید؟";
                        if (FarsiMessegeBox.Show(message, "حذف آگهی", FMessegeBoxButtons.YesNo, FMessegeBoxIcons.Question) == DialogResult.Yes)
                        {
                            adv = AdvertiseBussines.Change_Status(GroupGuid, false);
                            await adv.SaveAsync();
                            await LoadGroups();
                        }

                        return;
                    }

                }
                WebErrorLog.ErrorInstence.StartErrorLog("گروه یا آگهی انتخابی معتبر نمی باشد",false);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        private void Make_Picture_Boxes(List<string> lst)
        {
            try
            {
                if (lst == null || lst.Count == 0)
                    return;
                fPanel.AutoScroll = true;
                for (var i = fPanel.Controls.Count - 1; i >= 0; i--)
                    fPanel.Controls[i].Dispose();
                for (var i = 0; i < lst.Count; i++)
                {
                    var picbox = new PictureBox();
                    this.Controls.Add(picbox);
                    picbox.Size = new Size(62, 63);
                    picbox.Load(lst[i]);
                    picbox.Name = "pic" + i;
                    picbox.Cursor = Cursors.Hand;
                    picbox.SizeMode = PictureBoxSizeMode.StretchImage;
                    picbox.Click += picbox_Click;
                    fPanel.Controls.Add(picbox);
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        private void picbox_Click(object sender, EventArgs e)
        {
            try
            {
                var imageLocation = ((PictureBox)sender).ImageLocation;
                _picNameJari = ((PictureBox)sender).Name;
                if (_picNameJari == _pictureNameForClick)
                {
                    ((PictureBox)sender).BackColor = Color.Transparent;
                    ((PictureBox)sender).Padding = new Padding(-1);
                    _pictureNameForClick = null;
                    lstList.Add(imageLocation);
                    _orGpicBox = null;
                    return;
                }

                ((PictureBox)sender).BackColor = Color.Red;
                ((PictureBox)sender).Padding = new Padding(1);
                _pictureNameForClick = ((PictureBox)sender).Name;
                lstList.Remove(imageLocation);
                _orGpicBox = (PictureBox)sender;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        private void txtName_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtName);
        }

        private void txtPrice_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtPrice);
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtPrice);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtName);
        }
        private static void ShowBigSizePic(PictureBox pic)
        {
            try
            {
                pic.Size = new Size(190, 212);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private static void ShowNormalSizePic(PictureBox pic)
        {
            try
            {
                pic.Size = new Size(62, 63);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private void FillTxtTitles(List<string> sortedTitles)
        {
            txtTitles.Text = "";
            foreach (var title in sortedTitles)
            {
                if (txtTitles.Text != "") txtTitles.Text += Environment.NewLine;
                txtTitles.Text += title.Trim('*', '/', '\\', '-', '_', '~', '!', '@', '#', '$', '%', '^', '&', '+').Trim();
            }
        }

        private void btnDeleteImage_Click(object sender, EventArgs e)
        {
            try
            {
                Make_Picture_Boxes(lstList);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private void btnInsImage_Click(object sender, EventArgs e)
        {
            var t = new Thread(() =>
            {
                OpenFileDialog ofd = new OpenFileDialog { Multiselect = true, RestoreDirectory = true };
                if (ofd.ShowDialog() != DialogResult.OK) return;
                foreach (var name in ofd.FileNames)
                    lstList.Add(name);
            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            Make_Picture_Boxes(lstList);
        }

        private async void cmbDivarCat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var divarCat3 = await AdvCategoryBussines.GetAllAsync((Guid)cmbDivarCat2.SelectedValue, AdvertiseType.Divar);
                divarCat3 = divarCat3.OrderBy(q => q.Name).ToList();
                DivarCat3BingingSource.DataSource = divarCat3;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            txtSetter.Three_Ziro(txtPrice);
        }

        private async void cmbDivarCat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var divarCat2 = await AdvCategoryBussines.GetAllAsync((Guid)cmbDivarCat1.SelectedValue, AdvertiseType.Divar);
                divarCat2 = divarCat2.OrderBy(q => q.Name).ToList();
                DivarCat2BingingSource.DataSource = divarCat2;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbSheypoorCat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var sheypoorCat2 = await AdvCategoryBussines.GetAllAsync((Guid)cmbSheypoorCat1.SelectedValue, AdvertiseType.Sheypoor);
                sheypoorCat2 = sheypoorCat2.OrderBy(q => q.Name).ToList();
                SheypoorCat2BingingSource.DataSource = sheypoorCat2;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //txtSetter.KeyPress_Whit_Dot(sender,e);
        }

        private void frmAds_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        btnFinish.PerformClick();
                        return;
                    case Keys.Escape:
                        this.Close();
                        return;
                    case Keys.F8:
                        if (_orGpicBox != null)
                        {
                            ShowBigSizePic(_orGpicBox);
                            _fakepicBox = _orGpicBox;
                            _orGpicBox = null;
                        }
                        else
                        {
                            ShowNormalSizePic(_fakepicBox);
                            _orGpicBox = _fakepicBox;
                        }

                        break;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        private async Task<List<string>> CheckValidation()
        {
            try
            {
                string error = null;
                var lstErrors = new List<string>();

                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    error = "وارد کردن نام آگهی الزامی است";
                    lstErrors.Add(error);
                }

                if (!AdvertiseBussines.Check_Name(txtName.Text, _adv.Guid))
                {
                    error = "نام وارد شده برای آگهی تکراری می باشد";
                    lstErrors.Add(error);
                }

                if (txtTitles.Text.Length < 10)
                {
                    error = "وارد کردن حداقل 10 کاراکتر برای عنوان آگهی الزامی است";
                    lstErrors.Add(error);
                }

                List<string> lstTitles = new List<string>();
                lstTitles.Clear();
                lstTitles = GetTxtTitles();
                FillTxtTitles(lstTitles);


                if (string.IsNullOrWhiteSpace(txtTitles.Text))
                {
                    error = "وارد کردن عنوان آگهی الزامی است";
                    lstErrors.Add(error);
                }

                for (var i = 1; i <= lstTitles.Count; i++)
                {
                    if (lstTitles[i - 1].Length > 40)
                    {
                        error = $" سطر {i}  عناوین {lstTitles[i - 1].Length} حرف است. حداکثر 40 حرف باشد";
                        lstErrors.Add(error);
                    }

                    if (lstTitles[i - 1].Length < 10)
                    {
                        error = $" سطر {i}  عناوین {lstTitles[i - 1].Length} حرف است. حداقل 10 حرف باشد";
                        lstErrors.Add(error);
                    }


                }

                //چک کردن عناوین مشابه

                for (var i = 1; i <= lstTitles.Count; i++)
                    for (var j = i + 1; j <= lstTitles.Count; j++)
                        if (lstTitles[i - 1] == lstTitles[j - 1])
                            lstErrors.Add($"عنوان سطر {i} با عنوان سطر {j} مشابه است");
                //////////////////////////////////////////////////////////////////////////

                for (var i = 1; i < lstContent.Count; i++)
                {
                    if (lstContent[i - 1].Length < 20)
                    {
                        error = $"محتوای آگهی {txtDesc.Text.Length} حرف است. نباید کمتر از 20 کاراکتر باشد";
                        lstErrors.Add(error);
                    }
                    else if (lstContent[i - 1].Length > 900)
                    {
                        error = $"محتوای آگهی {lstContent[i - 1].Length} حرف است. نباید بیشتر از 900 کاراکتر باشد";
                        lstErrors.Add(error);
                    }
                }




                if (string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    error = "وارد کردن قیمت آگهی الزامی است";
                    lstErrors.Add(error);
                }


                return lstErrors;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }
        private List<string> GetTxtTitles()
        {
            var lstTitles = new List<string>();
            for (var i = 0; i < txtTitles.Lines.Count(); i++)
            {
                var newTitle = txtTitles.Lines[i].Trim('*', '/', '\\', '-', '_', '~', '!', '@', '#', '$', '%', '^', '&', '+').Trim();
                if (newTitle != "") lstTitles.Add(newTitle);
            }
            return lstTitles;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                btnFinish.Enabled = false;
                if (_adv.Guid == Guid.Empty)
                {
                    _adv.Guid = Guid.NewGuid();
                    _adv.DateSabt = DateConvertor.M2SH(DateTime.Now);
                }

                var checker = await CheckValidation();
                if (checker.Count > 0)
                {
                    var msg = "";
                    for (var i = 0; i < checker.Count; i++)
                        msg += checker[i] + "\r\n";
                    FarsiMessegeBox.Show(msg.Replace("\r\n", "'\n'"));
                    return;
                }

                for (var i = fPanel.Controls.Count - 1; i >= 0; i--)
                    fPanel.Controls[i].Dispose();

                var lstTitles = new List<string>();
                lstTitles.Clear();
                lstTitles = GetTxtTitles();
                FillTxtTitles(lstTitles);

                var direc = Path.Combine(Application.StartupPath, "AdvPictures");
                if (!Directory.Exists(direc)) Directory.CreateDirectory(direc);

                var lstT = new List<AdvTitlesBussines>();
                foreach (var item in lstTitles)
                {
                    var a = new AdvTitlesBussines()
                    {
                        Guid = Guid.NewGuid(),
                        DateSabt = DateConvertor.M2SH(DateTime.Now),
                        Status = true,
                        Title = item,
                        AdvGuid = _adv.Guid
                    };
                    lstT.Add(a);
                }

                var lstI = new List<AdvPicturesBussines>();
                foreach (var item in lstList)
                {
                    var a = new AdvPicturesBussines()
                    {
                        Guid = Guid.NewGuid(),
                        DateSabt = DateConvertor.M2SH(DateTime.Now),
                        Status = true,
                        PathGuid = item,
                        AdvGuid = _adv.Guid
                    };
                    lstI.Add(a);
                }

                var lstC = new List<AdvContentBussines>();
                foreach (var item in lstContent)
                {
                    var a = new AdvContentBussines()
                    {
                        Guid = Guid.NewGuid(),
                        DateSabt = DateConvertor.M2SH(DateTime.Now),
                        Status = true,
                        Content = item,
                        AdvGuid = _adv.Guid
                    };
                    lstC.Add(a);
                }

                _adv.AdvName = txtName.Text.Trim();
                _adv.Price = txtPrice.Text;
                _adv.Status = true;
                _adv.GroupGuid = (Guid)cmbGroup.SelectedValue;
                _adv.DivarCatGuid1 = (Guid)cmbDivarCat1.SelectedValue;
                _adv.DivarCatGuid2 = (Guid)cmbDivarCat2.SelectedValue;
                _adv.DivarCatGuid3 = (Guid?) cmbDivarCat3?.SelectedValue ?? Guid.Empty;
                _adv.SheypoorCatGuid1 = (Guid)cmbSheypoorCat1.SelectedValue;
                _adv.SheypoorCatGuid2 = (Guid)cmbSheypoorCat2.SelectedValue;

                await _adv.SaveAsync(lstT, lstI, lstC);

                _adv = new AdvertiseBussines();
                grpAccount.Enabled = false;
                btnInsImage.Enabled = false;
                btnDeleteImage.Enabled = false;
                btnFinish.Text = "تایید (F5)";
                await Set_Data();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
            finally
            {
                btnFinish.Enabled = true;
            }
        }

        private void mnuInsAdv_Click(object sender, EventArgs e)
        {
            try
            {
                _adv = new AdvertiseBussines();
                grpAccount.Enabled = true;
                btnInsImage.Enabled = true;
                btnDeleteImage.Enabled = true;
                btnFinish.Text = "تایید (F5)";
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnInsNewContent_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Back();
        }
        public void Next()
        {
            try
            {
                if (top >= lstContent.Count - 1) return;
                top++;
                txtDesc.Text = lstContent[top];
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        public void Back()
        {
            try
            {
                if (top <= 0 || top - 1 > lstContent.Count) return;
                top--;
                txtDesc.Text = lstContent[top];
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private void btnSaveInList_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstContent.Count == top)
                {
                    lstContent.Add(txtDesc.Text);
                    top++;
                }
                else
                {
                    lstContent.RemoveAt(top);
                    lstContent.Add(txtDesc.Text);
                    top++;
                }
                txtDesc.Text = "";
                top = lstContent.Count;
                txtDesc.Focus();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            top = lstContent.Count;
            txtDesc.Text = "";
        }
    }
}
