using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;

namespace HI.ST
{
    public partial class wUserLogIn : DevExpress.XtraEditors.XtraForm
    {

        private bool _ProcNew = false;


        public wUserLogIn()
        {
            InitializeComponent();
      
            _ProcNew = true;

            ProcLoadLang();
            ProcLoadCmp();

            FNLang.SelectedIndex = ((int)HI.ST.Lang.Language - 1);
            _ProcNew = false;
        }

#region "Procedure"

        private void ProcLoadLang()
	{
		string _Strsql = null;
		DataTable _Dt = null;
		_Strsql = " SELECT     FTListName, FNListIndex, FTNameTH, FTNameEN";
		_Strsql += Constants.vbCrLf + "  FROM   [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SYSTEM) + "].dbo.HSysListData WITH(NOLOCK) ";
		_Strsql += Constants.vbCrLf + " WHERE FTListName='FNLang'  ORDER BY FTListName, FNListIndex ";

		_Dt = HI.Conn.SQLConn.GetDataTable(_Strsql, Conn.DB.DataBaseName.DB_SYSTEM);
	
		this.FNLang.Properties.Items.Clear();
		int _ImgIndex = 0;
		string[] _arr = new string[15];


		foreach (DataRow R in _Dt.Rows) {
			if (HI.ST.Lang.Language == Lang.eLang.TH) {
				_arr[_ImgIndex] = (R["FTNameTH"]).ToString();
			} else {
				_arr[_ImgIndex] = (R["FTNameEN"]).ToString();
			}

			_ImgIndex = _ImgIndex + 1;

		}

        this.FNLang.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] { new DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr[0], 0), new DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr[1], 1), new DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr[2], 2), new DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr[3], 3), new DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr[4], 4), new DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr[5], 5), new DevExpress.XtraEditors.Controls.ImageComboBoxItem(_arr[6], 6) });
		this.FNLang.SelectedIndex = ((int)HI.ST.Lang.Language - 1);

	}

	private List<ComBoList> _ListCmp = new List<ComBoList>();
	private void ProcLoadCmp()
	{
		_ListCmp.Clear();

		string _TmpStrTH = "";
		string _TmpStrEN = "";
		string _TmpStrValue = "";
		string _TmpStrRefer = "";

		string _Strsql = null;
		DataTable _Dt = null;
		_Strsql = " SELECT    FNHSysCmpId, FTCmpCode,FTDocRun ";
		_Strsql += Constants.vbCrLf + "  FROM  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp WITH(NOLOCK) ";
		_Strsql += Constants.vbCrLf + " Order By FTCmpCode";

		_Dt = HI.Conn.SQLConn.GetDataTable(_Strsql, Conn.DB.DataBaseName.DB_SYSTEM);

        this.FNHSysCmpId.Properties.Items.Clear();

		_TmpStrTH = "";
		_TmpStrEN = "";
		_TmpStrValue = "";
		_TmpStrRefer = "";

		foreach (DataRow Row in _Dt.Rows) {
		
			if (string.IsNullOrEmpty(_TmpStrTH)) {
				_TmpStrTH = (Row["FTCmpCode"]).ToString();
				_TmpStrEN = (Row["FTCmpCode"]).ToString();
				_TmpStrValue = (Row["FNHSysCmpId"]).ToString();
				_TmpStrRefer = (Row["FTDocRun"]).ToString();
			} else {
				_TmpStrTH = _TmpStrTH + "|" + (Row["FTCmpCode"]).ToString();
				_TmpStrEN = _TmpStrEN + "|" + (Row["FTCmpCode"]).ToString();
				_TmpStrValue = _TmpStrValue + "|" + (Row["FNHSysCmpId"]).ToString();
				_TmpStrRefer = _TmpStrRefer + "|" + (Row["FTDocRun"]).ToString();
			}
		}

		ComBoList M = new ComBoList();
		M.ListName = "Cmp";
		M.ListEN = _TmpStrEN.Split('|');
		M.ListTH = _TmpStrTH.Split('|');
		M.ListValue = _TmpStrValue.Split('|');
		M.ListRefer = _TmpStrRefer.Split('|');

		_ListCmp.Add(M);

        this.FNHSysCmpId.Properties.Items.AddRange(_ListCmp[0].ListTH);


		if (!string.IsNullOrEmpty(HI.ST.SysInfo.CmpDefualtCode)) {
			try {
                this.FNHSysCmpId.Text = HI.ST.SysInfo.CmpDefualtCode;
			} catch (Exception ) {
                this.FNHSysCmpId.SelectedIndex = -1;
			}
		} else {
			try {
                this.FNHSysCmpId.SelectedIndex = 0;
			} catch (Exception ) {
                this.FNHSysCmpId.SelectedIndex = -1;
			}
		}
	}

	private string GetListValue(int Index)
	{
		string Str = "";
		try {
			Str = _ListCmp[0].ListValue[Index];
			return Str;
		} catch (Exception ex) {
		}
		if (string.IsNullOrEmpty(Str))
			Str = Index.ToString();
		return Str;
	}

	private string GetListRefer(int Index)
	{
		string Str = "";
		try {
			Str = _ListCmp[0].ListRefer[Index];
			return Str;
		} catch (Exception ex) {
		}
		if (string.IsNullOrEmpty(Str))
			Str = Index.ToString();
		return Str;
	}

	private class ComBoList
	{

        public string ListName{get; set;}
        public string[] ListEN{get;set;}
        public string[] ListTH{get;set;}
        public string[] ListValue{ get; set;}
        public string[] ListRefer{get; set; }

	}
	#endregion

	#region "General"

    public bool Confirm { get; set;}

	public bool CheckUserLogin(ref string LoginUser, ref string Password, ref string UserCompany)
	{
		try {
            Confirm = false;
			this.ShowDialog();
			LoginUser = otbLogin.Text.Trim();
			Password = otbPassword.Text.Trim();
			UserCompany = FNHSysCmpId.Text;
		} catch (Exception ex) {
			throw new Exception(ex.Message);
		}

        return Confirm;

	}


	private void FNLang_SelectedIndexChanged(System.Object sender, System.EventArgs e)
	{

		if (!(_ProcNew)) {
            HI.ST.Lang.Language = (HI.ST.Lang.eLang)((FNLang.SelectedIndex + 1));

			if (string.IsNullOrEmpty(otbLogin.Text.Trim())) {
				otbLogin.Focus();
				return;
			}

			if (string.IsNullOrEmpty(otbPassword.Text.Trim())) {
				otbPassword.Focus();
				return;
			}

			HI.UL.AppRegistry.WriteRegistry(UL.AppRegistry.KeyName.Language, HI.ST.Lang.Language);

			if (this.VerifyLogin()) {
                Confirm = true;
				this.Close();
			}

		}
	}

	private void otbLogin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
	{

            switch (e.KeyCode) {
			case System.Windows.Forms.Keys.Enter:
				if (string.IsNullOrEmpty(otbLogin.Text.Trim())) {
					otbLogin.Focus();
					return;
				}

				if (string.IsNullOrEmpty(otbPassword.Text.Trim())) {
					otbPassword.Focus();
					return;
				}

				if (this.VerifyLogin()) {
                    Confirm = true;
					this.Close();
				}
				break;
			case System.Windows.Forms.Keys.Escape:
                Confirm = false;
				this.Close();
				break;
		}

	}

	private void FNHSysCmpId_SelectedIndexChanged(System.Object sender, System.EventArgs e)
	{
		try {
			HI.ST.SysInfo.CmpCode = FNHSysCmpId.Text;
			HI.ST.SysInfo.CmpID = (int)Conversion.Val(GetListValue(FNHSysCmpId.SelectedIndex));
			HI.ST.SysInfo.CmpRunID = GetListRefer(FNHSysCmpId.SelectedIndex);

		} catch (Exception ex) {
		}
	}

	private bool VerifyLogin( )
	{

		bool _Verify = false;
		try {

           string mUserName  = this.otbLogin.Text;
            HI.ST.UserInfo.UserName = "";// this.otbLogin.Text;
			HI.ST.UserInfo.UserPassword = this.otbPassword.Text;
			HI.ST.UserInfo.UserCompany = this.FNHSysCmpId.Text;

            HI.ST.SysInfo.ADUserName = "";

                if (HI.Conn.DB.UsedADUser && HI.Conn.DB.UsedADUserIP != "")



                {

                    try {
                        System.DirectoryServices.AccountManagement.UserPrincipal currentADUser;
                        currentADUser = System.DirectoryServices.AccountManagement.UserPrincipal.Current;
                    } catch { }
                   

                    if (GetUserActiveDirectory(HI.Conn.DB.UsedADUserIP, this.otbLogin.Text, this.otbPassword.Text)) {

                        string _Sql = null;
                        _Sql = "SELECT TOP 1  *  FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLogin ";
                        _Sql += Constants.vbCrLf + " WHERE FTUserAD='" + HI.UL.ULF.rpQuoted(HI.ST.SysInfo.ADUserName) + "'";

                        DataTable _dt = HI.Conn.SQLConn.GetDataTable(_Sql, Conn.DB.DataBaseName.DB_SECURITY, null, false);

                        if (_dt.Rows.Count > 0)
                        {
                            foreach (DataRow R in _dt.Rows)
                            {

                                HI.ST.UserInfo.UserName =(R["FTUserName"]).ToString();
                                HI.ST.UserInfo.UserPassword = HI.Conn.DB.FuncDecryptData((R["FTPassword"]).ToString());
                              
                                    if ((R["FTStateActive"]).ToString() == "1")
                                    {
                                        _Verify = true;

                                        HI.ST.SysInfo.Admin = ((R["FTStateAdmin"]).ToString() == "1");
                                        HI.ST.SysInfo.AdminAllModule = ((R["FTStateAdminFollowModule"]).ToString() != "1"); //(string.IsNullOrEmpty((R["FTStateAdminFollowModule"]).ToString()));
                                        HI.Conn.DB.UserNameLogIn = HI.ST.UserInfo.UserName;

                                        if (!(HI.ST.SysInfo.Admin))
                                        {

                                            _Sql = "  SELECT TOP 1   A.FTUserName, C.FTCmpCode";
                                            _Sql += Constants.vbCrLf + " FROM            [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS A WITH(NOLOCK) INNER JOIN";
                                            _Sql += Constants.vbCrLf + "  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionCmp AS B WITH(NOLOCK)  ON A.FNHSysPermissionID = B.FNHSysPermissionID INNER JOIN";
                                            _Sql += Constants.vbCrLf + " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp AS C WITH(NOLOCK)  ON B.FNHSysCmpId = C.FNHSysCmpId";
                                            _Sql += Constants.vbCrLf + " WHERE A.FTUserName='" + HI.ST.UserInfo.UserName + "'";
                                            _Sql += Constants.vbCrLf + " AND  C.FTCmpCode='" + HI.ST.UserInfo.UserCompany + "'";

                                            if (string.IsNullOrEmpty(HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_SECURITY, "")))
                                            {
                                                _Verify = false;
                                                Interaction.MsgBox("Username Can not Access This Company Please Contact System Admin !!!");

                                            }

                                        }

                                        try
                                        {
                                            HI.ST.UserInfo.UserImage = HI.UL.ULImage.ConvertByteArrayToImmage(R["FPUserImage"]);
                                        }
                                        catch (Exception ex)
                                        {
                                            HI.ST.UserInfo.UserImage = null;
                                        }

                                    }
                                    else
                                    {
                                        Interaction.MsgBox("Username is Not Active Please Contact System Admin !!!");
                                    }
                         
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                        else
                        {
                            Interaction.MsgBox("ไม่พบข้อมูลการกำหนดสิทธิเข้าใช้งานระบบ Wisdom !!!");
                            otbLogin.Focus();
                            otbLogin.SelectAll();
                        }

                        _dt.Dispose();


                    }

                }
                else {
                    HI.ST.UserInfo.UserName = mUserName;
                    string _Sql = null;
                    _Sql = "SELECT TOP 1  *  FROM [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLogin ";
                    _Sql += Constants.vbCrLf + " WHERE FTUserName='" + HI.ST.UserInfo.UserName + "'";

                    DataTable _dt = HI.Conn.SQLConn.GetDataTable(_Sql, Conn.DB.DataBaseName.DB_SECURITY, null, false);

                    if (_dt.Rows.Count > 0)
                    {
                        foreach (DataRow R in _dt.Rows)
                        {
                            if (HI.Conn.DB.FuncDecryptData((R["FTPassword"]).ToString()) == HI.ST.UserInfo.UserPassword)
                            {
                                if ((R["FTStateActive"]).ToString() == "1")
                                {
                                    _Verify = true;

                                    HI.ST.SysInfo.Admin = ((R["FTStateAdmin"]).ToString() == "1");
                                    HI.ST.SysInfo.AdminAllModule = ((R["FTStateAdminFollowModule"]).ToString() != "1"); //(string.IsNullOrEmpty((R["FTStateAdminFollowModule"]).ToString()));

                                    if (!(HI.ST.SysInfo.Admin))
                                    {
                                        _Sql = "  SELECT TOP 1   A.FTUserName, C.FTCmpCode";
                                        _Sql += Constants.vbCrLf + " FROM            [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEUserLoginPermission AS A WITH(NOLOCK) INNER JOIN";
                                        _Sql += Constants.vbCrLf + "  [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_SECURITY) + "].dbo.TSEPermissionCmp AS B WITH(NOLOCK)  ON A.FNHSysPermissionID = B.FNHSysPermissionID INNER JOIN";
                                        _Sql += Constants.vbCrLf + " [" + HI.Conn.DB.GetDataBaseName(Conn.DB.DataBaseName.DB_MASTER) + "].dbo.TCNMCmp AS C WITH(NOLOCK)  ON B.FNHSysCmpId = C.FNHSysCmpId";
                                        _Sql += Constants.vbCrLf + " WHERE A.FTUserName='" + HI.ST.UserInfo.UserName + "'";
                                        _Sql += Constants.vbCrLf + " AND  C.FTCmpCode='" + HI.ST.UserInfo.UserCompany + "'";

                                        if (string.IsNullOrEmpty(HI.Conn.SQLConn.GetField(_Sql, Conn.DB.DataBaseName.DB_SECURITY, "")))
                                        {
                                            _Verify = false;
                                            Interaction.MsgBox("Username Can not Access This Company Please Contact System Admin !!!");
                                        }

                                    }

                                    try
                                    {
                                        HI.ST.UserInfo.UserImage = HI.UL.ULImage.ConvertByteArrayToImmage(R["FPUserImage"]);
                                    }
                                    catch (Exception ex)
                                    {
                                        HI.ST.UserInfo.UserImage = null;
                                    }

                                }
                                else
                                {
                                    Interaction.MsgBox("Username is Not Active Please Contact System Admin !!!");
                                }
                            }
                            else
                            {
                                Interaction.MsgBox("Password is Incorrect !!!");
                                otbPassword.Focus();
                                otbPassword.SelectAll();
                            }
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    else
                    {
                        Interaction.MsgBox("Username is Incorrect !!!");
                        otbLogin.Focus();
                        otbLogin.SelectAll();
                    }

                    _dt.Dispose();

                }
          

         
		} catch (Exception ex) {
			throw new Exception(ex.Message);
		}

		return _Verify;
	}



     

        #endregion


        private  bool GetUserActiveDirectory(string domain,string username, string password)
        {

            string msgbox = "";
            bool StaetSuccess = false;
            System.DirectoryServices.DirectoryEntry objDE = new System.DirectoryServices.DirectoryEntry("LDAP://" + domain, username, password);
            //System.DirectoryServices.DirectorySearcher Searcher = new System.DirectoryServices.DirectorySearcher(objDE);


            //Searcher.SearchScope = System.DirectoryServices.SearchScope.OneLevel;
            //System.DirectoryServices.SearchResultCollection Results = Searcher.FindAll();
            //StaetSuccess = !(Results == null);


            //// Login Name
            //Console.WriteLine(GetProperty(sResultSet, "cn"));
            //// First Name
            //Console.WriteLine(GetProperty(sResultSet, "givenName"));
            //// Middle Initials
            //Console.Write(GetProperty(sResultSet, "initials"));
            //// Last Name
            //Console.Write(GetProperty(sResultSet, "sn"));
            //// Address
            //string tempAddress = GetProperty(sResultSet, "homePostalAddress");

            //if (tempAddress != string.Empty)
            //{
            //    string[] addressArray = tempAddress.Split(';');
            //    string taddr1, taddr2;
            //    taddr1 = addressArray[0];
            //    Console.Write(taddr1);
            //    taddr2 = addressArray[1];
            //    Console.Write(taddr2);
            //}
            //// title
            //Console.Write(GetProperty(sResultSet, "title"));
            //// company
            //Console.Write(GetProperty(sResultSet, "company"));
            ////state
            //Console.Write(GetProperty(sResultSet, "st"));
            ////city
            //Console.Write(GetProperty(sResultSet, "l"));
            ////country
            //Console.Write(GetProperty(sResultSet, "co"));
            ////postal code
            //Console.Write(GetProperty(sResultSet, "postalCode"));
            //// telephonenumber
            //Console.Write(GetProperty(sResultSet, "telephoneNumber"));
            ////extention
            //Console.Write(GetProperty(sResultSet, "otherTelephone"));
            ////fax
            //Console.Write(GetProperty(sResultSet, "facsimileTelephoneNumber"));

            //// email address
            //Console.Write(GetProperty(sResultSet, "mail"));
            //// Challenge Question
            //Console.Write(GetProperty(sResultSet, "extensionAttribute1"));
            //// Challenge Response
            //Console.Write(GetProperty(sResultSet, "extensionAttribute2"));
            ////Member Company
            //Console.Write(GetProperty(sResultSet, "extensionAttribute3"));
            //// Company Relation ship Exits
            //Console.Write(GetProperty(sResultSet, "extensionAttribute4"));
            ////status
            //Console.Write(GetProperty(sResultSet, "extensionAttribute5"));
            //// Assigned Sales Person
            //Console.Write(GetProperty(sResultSet, "extensionAttribute6"));
            //// Accept T and C
            //Console.Write(GetProperty(sResultSet, "extensionAttribute7"));
            //// jobs
            //Console.Write(GetProperty(sResultSet, "extensionAttribute8"));
            //String tEamil = GetProperty(sResultSet, "extensionAttribute9");

            //// email over night
            //if (tEamil != string.Empty)
            //{
            //    string em1, em2, em3;
            //    string[] emailArray = tEmail.Split(';');
            //    em1 = emailArray[0];
            //    em2 = emailArray[1];
            //    em3 = emailArray[2];
            //    Console.Write(em1 + em2 + em3);

            //}
            //// email daily emerging market
            //Console.Write(GetProperty(sResultSet, "extensionAttribute10"));
            //// email daily corporate market
            //Console.Write(GetProperty(sResultSet, "extensionAttribute11"));
            //// AssetMgt Range
            //Console.Write(GetProperty(sResultSet, "extensionAttribute12"));
            //// date of account created
            //Console.Write(GetProperty(sResultSet, "whenCreated"));
            //// date of account changed
            //Console.Write(GetProperty(sResultSet, "whenChanged"));


            using (objDE)
            {

                DirectorySearcher objDSearcher = new DirectorySearcher();
                objDSearcher.SearchRoot = objDE;
                objDSearcher.PropertiesToLoad.Add("department");
                objDSearcher.PropertiesToLoad.Add("title");
                objDSearcher.PropertiesToLoad.Add("cn");
                objDSearcher.PropertiesToLoad.Add("SAMAccountName");
                objDSearcher.PropertiesToLoad.Add("givenname");
                objDSearcher.PropertiesToLoad.Add("sn");
                objDSearcher.PropertiesToLoad.Add("memberOf");
                objDSearcher.PropertiesToLoad.Add("department");
                objDSearcher.PropertiesToLoad.Add("title");
                objDSearcher.PropertiesToLoad.Add("postalCode");
                objDSearcher.PropertiesToLoad.Add("streetAddress");
                objDSearcher.PropertiesToLoad.Add("st");
                objDSearcher.PropertiesToLoad.Add("telephoneNumber");
                objDSearcher.PropertiesToLoad.Add("l");
                objDSearcher.PropertiesToLoad.Add("mail");
                objDSearcher.PropertiesToLoad.Add("initials");
                objDSearcher.PropertiesToLoad.Add("company");
                objDSearcher.PropertiesToLoad.Add("co");
                objDSearcher.PropertiesToLoad.Add("otherTelephone");
                objDSearcher.PropertiesToLoad.Add("facsimileTelephoneNumber");
                objDSearcher.PropertiesToLoad.Add("whenCreated");
                objDSearcher.PropertiesToLoad.Add("whenChanged");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute1");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute2");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute3");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute4");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute5");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute6");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute7");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute8");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute9");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute10");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute11");
                objDSearcher.PropertiesToLoad.Add("extensionAttribute12");

                objDSearcher.Filter = "(SAMAccountName=" + username + ")";

                objDSearcher.SearchScope = SearchScope.Subtree;

                try
                {
                    SearchResultCollection mResults = objDSearcher.FindAll();

                    StaetSuccess = !(mResults == null);

                    foreach (SearchResult sr in mResults)
                    {

                        string SAMAccountName = sr.Properties["SAMAccountName"][0].ToString();
                        string givenname = sr.Properties["givenname"][0].ToString();
                        string sn = sr.Properties["sn"][0].ToString();
                        
                        //Response.Write("ID:" + sr.Properties["SAMAccountName"][0].ToString() + "<br/>");
                        //Response.Write("ID:" + sr.Properties["givenname"][0].ToString() + "<br/>");
                        //Response.Write("ID:" + sr.Properties["cn"][0].ToString() + "<br/>");
                        //Response.Write("Department:" + sr.Properties["Department"][0].ToString() + "<br/>");
                        //Response.Write("title:" + sr.Properties["title"][0].ToString() + "<br/>");
                        //Response.Write("------------------------------------------------------------------------<br/>");

                        HI.ST.SysInfo.ADUserName = SAMAccountName;

                        break;

                    }


             
                }
                catch (System.DirectoryServices.DirectoryServicesCOMException ex)

                {
                     msgbox = ex.Message + "    กรุณาติดต่อแผนก Information Technology. เพื่อตรวจสอบ User Active Directory ";

                    Interaction.MsgBox(msgbox);
                }
                catch (Exception ex)
                {

                     msgbox = ex.Message + "    กรุณาติดต่อแผนก Information Technology. เพื่อตรวจสอบ User Active Directory ";
                    Interaction.MsgBox(msgbox);
                }

            }


            if (StaetSuccess == false && msgbox =="") {

                 msgbox = " User เข้าใช้งาน  Active Directory ไม่ถูกต้อง  กรุณาติดต่อแผนก Information Technology. เพื่อตรวจสอบ User Active Directory !!! ";
                Interaction.MsgBox(msgbox);
            }

            return StaetSuccess;
        }

        private void opmexit_Click(object sender, EventArgs e)
	{
        Confirm = false;
		this.Close();
    }

        private void otbLogin_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void otbPassword_EditValueChanged(object sender, EventArgs e)
        {

        }

        private static string GetProperty(SearchResult searchResult,string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}