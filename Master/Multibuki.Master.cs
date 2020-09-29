using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MultibukiWeb.Master
{
    public partial class Multibuki : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lbtnKorisnik.Text = Session["Korisnik_Ime"].ToString() + ' ' + Session["Korisnik_Prezime"].ToString();

                if (Session["Slika"] == null)
                    Image1.ImageUrl = "~/Images/user.png";
                else
                    Image1.ImageUrl = "data:Image/png;base64," + Session["Slika"];

                Label3.Text = Session["Firma_Opis"].ToString();
                Label4.Text= Session["Firma_Opis1"].ToString();
                //if (Session["Menu"] != null)
                //{
                //    string xmlStr = Session["Menu"].ToString();
                //    if (xmlStr == "" || xmlStr == "<dsIzlaz/>")
                //        xmlStr = "<siteMap></siteMap>";
                //    menuXMLDS.Data = "";
                //    menuXMLDS.Data = xmlStr;

                //    OsnovniMenu.Items.Clear();
                //    OsnovniMenu.DataBind();
                //    OsnovniMenu.Attributes.Add("MenuItemClick", "OsnovniMenu_MenuItemClick");

                //}
                //else
                //    lblStatus.Text = "Nemate pravo na izabranu opciju" +
                //        "";

                GetTreeViewItems();

                reloadTreeviewState();
             

            }

        }

        private void PozoviJavascript(string script)
        {
            try
            {
                Page page1 = (Page)HttpContext.Current.Handler;
                ScriptManager.RegisterStartupScript(page1, typeof(Page), "ObradaiObavestenje", script, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void OsnovniMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            lblNaslov.Text = e.Item.Text;
        }

        protected void lnkPomoc_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnKorisnik_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/aaaWebForms/aaaKorisnik.aspx");
        }

        protected void lbtnOdjava_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            ExpireAllCookies();
            Response.Redirect("../aaaWebForms/aaaPrijava.aspx");
        }

        private void PrikaziObavestenje(string poruka)
        {
            try
            {
                poruka = (poruka.Replace("\n", "")).Replace("'", "");
                poruka = (poruka.Replace("\r", "")).Replace("'", "");
                string script = "alert('" + poruka + "');";
                Page page1 = (Page)HttpContext.Current.Handler;
                ScriptManager.RegisterStartupScript(page1, typeof(Page), "Прикази обавештење", script, true);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
            }
        }

        //public promenljiva da bi se videla iz metode GetTreeViewItems u GetChildRows
        public TreeNode parentTreeNode;

        private void GetTreeViewItems()
        {
            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();

            int IDKorisnik = int.Parse(Session["Korisnik_IDKorisnik"].ToString());

            wcfMultibuki.GetTreeViewItemsRequest zahtev = new wcfMultibuki.GetTreeViewItemsRequest(IDKorisnik);
            wcfMultibuki.GetTreeViewItemsResponse odgovor = new wcfMultibuki.GetTreeViewItemsResponse();           

            try
            {
                odgovor = client.GetTreeViewItems(zahtev);
            }
            catch (Exception ex)
            {
                PrikaziObavestenje("Greška pri pozivu servisa u proceduri GetTreeViewItems!  \\n" + ex.Message);
                return;
            }

            try
            {
                if (odgovor.GetTreeViewItemsResult.dtGreska.Rows.Count > 0)
                    PrikaziObavestenje(odgovor.GetTreeViewItemsResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    odgovor.GetTreeViewItemsResult.Relations.Add("ChildRows", odgovor.GetTreeViewItemsResult.dtGetTreeViewItems.Columns["IDFunkcija"], odgovor.GetTreeViewItemsResult.dtGetTreeViewItems.Columns["IDFunkcija_Nadredjena"]);

                    foreach (DataRow level1DataRow in odgovor.GetTreeViewItemsResult.dtGetTreeViewItems.Rows)
                    {
                        if (string.IsNullOrEmpty(level1DataRow["IDFunkcija_Nadredjena"].ToString()))
                        {
                            parentTreeNode = new TreeNode();
                            parentTreeNode.Text = level1DataRow["NazivFunkcije"].ToString();
                            parentTreeNode.NavigateUrl = level1DataRow["Url"].ToString();

                            GetChildRows(level1DataRow, parentTreeNode);

                            TreeView1.Nodes.Add(parentTreeNode);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                PrikaziObavestenje("Greška u aplikaciji u metodi GetTreeViewItems  \\n" + ex.Message);
                return;
            }

            //string cs = ConfigurationManager.ConnectionStrings["KategorizacijaPSConnectionString"].ConnectionString;
            //SqlConnection con = new SqlConnection(cs);
            //SqlDataAdapter da = new SqlDataAdapter("spGetTreeViewItems", con);
            //da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //DataSet ds = new DataSet();
            //da.Fill(ds);

            //ds.Relations.Add("ChildRows", ds.Tables[0].Columns["IDFunkcija"], ds.Tables[0].Columns["IDFunkcija_Nadredjena"]);

            //foreach (DataRow level1DataRow in ds.Tables[0].Rows)
            //{
            //    if (string.IsNullOrEmpty(level1DataRow["IDFunkcija_Nadredjena"].ToString()))
            //    {
            //        TreeNode treeNode = new TreeNode();
            //        treeNode.Text = level1DataRow["NazivFunkcije"].ToString();
            //        treeNode.NavigateUrl = level1DataRow["Url"].ToString();

            //        DataRow[] level2DataRows = level1DataRow.GetChildRows("ChildRows");
            //        foreach (DataRow level2DataRow in level2DataRows)
            //        {
            //            TreeNode childTreeNode = new TreeNode();
            //            childTreeNode.Text = level2DataRow["NazivFunkcije"].ToString();
            //            childTreeNode.NavigateUrl = level2DataRow["Url"].ToString();
            //            treeNode.ChildNodes.Add(childTreeNode);
            //        }
            //        Treeview1.Nodes.Add(treeNode);

            //        Treeview1.Font.Bold = true;
            //        Treeview1.ForeColor = System.Drawing.Color.DarkBlue;
            //    }
            //}
        }

        private void GetChildRows(DataRow dataRow, TreeNode treeNode)
        {
            DataRow[] childRows = dataRow.GetChildRows("ChildRows");
            foreach (DataRow childRow in childRows)
            {
                //▼
                parentTreeNode.Text = parentTreeNode.Text.Split('▾')[0];
                parentTreeNode.Text = parentTreeNode.Text + " " + "▾";

                TreeNode childTreeNode = new TreeNode();
                childTreeNode.Text = childRow["NazivFunkcije"].ToString();
                childTreeNode.NavigateUrl = childRow["Url"].ToString();
                treeNode.ChildNodes.Add(childTreeNode);
                if (childRow.GetChildRows("ChildRows").Length > 0)
                {
                    parentTreeNode.Text = parentTreeNode.Text.Split('▾')[0];
                    childTreeNode.Text = childTreeNode.Text.Split('▾')[0];

                    parentTreeNode.Text = parentTreeNode.Text + " " + "▾";
                    childTreeNode.Text = childRow["NazivFunkcije"].ToString() + " " + "▾";

                    GetChildRows(childRow, childTreeNode);
                }
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            //za visestepeno ako bude trebalo
            //if (TreeView1.SelectedNode.Parent != null)
            //{
            //    TreeView1.SelectedNode.Parent.Expand();
            //    if (TreeView1.SelectedNode.Parent.Parent != null)
            //        TreeView1.SelectedNode.Parent.Parent.Expand();

            //}
            //else
            //{


            bool? provera = TreeView1.SelectedNode.Expanded;

            if (provera == null || provera == false)
            {
                TreeView1.SelectedNode.Expand();
                //TreeView1.SelectedNode.Selected = false;
            }

            else
                TreeView1.SelectedNode.Collapse();

            TreeView1.SelectedNode.Selected = false;

            //}

        }

        public void reloadTreeviewState()
        {
            try
            {
                HttpCookie cookie = Request.Cookies["ToolsTVExpand"];
                if (cookie != null && cookie.Value != null)
                {
                    int currIdx = 0;
                    foreach (TreeNode mainNode in TreeView1.Nodes)
                    {
                        currIdx = setNodeStates(mainNode, cookie.Value, currIdx);
                    }
                }
            }
            catch (Exception e)
            {
                // Just keep going
            }
        }
        protected int setNodeStates(TreeNode node, String stateInfo, int currIdx)
        {
            if (currIdx >= stateInfo.Length)
                throw new Exception("State info shorter than list of nodes.");
            Char state = stateInfo[currIdx];
            if (state == 'e')
            {
                node.Expand();
            }
            currIdx++;
            if (node.ChildNodes != null && node.ChildNodes.Count != 0)
            {
                foreach (TreeNode childNode in node.ChildNodes)
                {
                    currIdx = setNodeStates(childNode, stateInfo, currIdx);
                }
            }

            return currIdx;
        }

        private void ExpireAllCookies()
        {

            //HttpCookie cookie = Request.Cookies["ToolsTVExpand"];
            //cookie.Expires = DateTime.Now.AddDays(-1);

            if (HttpContext.Current != null)
            {
                // HttpCookie cookie = Request.Cookies["ToolsTVExpand"];

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ToolsTVExpand"];
                if (cookie != null)
                {
                    var expiredCookie = new HttpCookie(cookie.Name)
                    {
                        Expires = DateTime.Now.AddDays(-1),
                        Domain = cookie.Domain
                    };
                    HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                }


                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }

            //if (HttpContext.Current != null)
            //{
            //    int cookieCount = HttpContext.Current.Request.Cookies.Count;
            //    for (var i = 0; i < cookieCount; i++)
            //    {
            //        var cookie = HttpContext.Current.Request.Cookies[i];
            //        if (cookie != null)
            //        {
            //            var expiredCookie = new HttpCookie(cookie.Name)
            //            {
            //                Expires = DateTime.Now.AddDays(-1),
            //                Domain = cookie.Domain
            //            };
            //            HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
            //        }
            //    }

            //    // clear cookies server side
            //    HttpContext.Current.Request.Cookies.Clear();
            //}
        }


     


    }
}