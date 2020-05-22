using SQSAdmin.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQSAdmin
{
	public partial class frmHomeDisplayOption : Form
	{
		frmPagLookup pagLookupForm;
		frmPAGWizard pagWizardForm;


		public frmHomeDisplayOption()
		{
			InitializeComponent();
		}

		private void bindingSource1_CurrentChanged(object sender, EventArgs e)
		{

		}

		private void frmHomeDisplayOption_Load(object sender, EventArgs e)
		{
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            loadStatusDropdown();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

		private void fillToolStripButton_Click(object sender, EventArgs e)
		{
			SearchPAGForHome();

		}

		private void SearchPAGForHome()
		{
            int pagID,active;

            try
            {
                pagID=Int32.Parse(productAreaGroupIDToolStripTextBox.Text.ToString());
                active = Int32.Parse(((ComboBox)toolStripComboBox1.Control).SelectedValue.ToString());
                DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminSearchHomeDisplayOption2]", new System.Data.SqlClient.SqlParameter[2]
                            {
                                new System.Data.SqlClient.SqlParameter("@ProductAreaGroupID", pagID),
                                new System.Data.SqlClient.SqlParameter("@active",active ),
                            }

                       );
                dataGridView1.DataSource = dsHome.Tables[0];
               
            }
            catch (SqlException ex2)
            {
                MessageBox.Show(ex2.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a valid pag ID! "+ex.ToString());
            }


            /*
			this.adminSearchHomeDisplayOptionTableAdapter.Connection.ConnectionString = MetriconCommon.getConnectionString();

			try
			{
				this.adminSearchHomeDisplayOptionTableAdapter.Fill(this.pMO006STGDataSet.AdminSearchHomeDisplayOption, new System.Nullable<long>(((long)(System.Convert.ChangeType(productAreaGroupIDToolStripTextBox.Text, typeof(long))))));
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
             * */




        }

		private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
            int active,active2,qty;
            bool so, si, changeprice, changeqty, addextradesc;
            qty = 0;
			try
			{
                if (dataGridView1.Rows.Count != 0)
                {
                    this.adminSearchHomeDisplayOptionTableAdapter.Connection.ConnectionString = MetriconCommon.getConnectionString();

                    DataRow modifiedRow = ((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;

                    DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select active from homedisplayoption_Staging where productareagroupid=" + modifiedRow["productareagroupid"].ToString() + " and homeid=" + modifiedRow["homeid"].ToString());
                    if (dsHome.Tables[0].Rows[0][0].ToString() == "False")
                    {
                        active = 0;
                    }
                    else
                    {
                        active = 1;
                    }
                    if (modifiedRow["active"].ToString()=="True")
                    {
                        active2=1;
                    }
                    else
                    {
                        active2 = 0;
                    }
                    //modifiedRow["EnterDesc"] = "Note: Stained finish to KDHW components on this stair are non-standard and cannot be offered." + DateTime.Now.ToLongTimeString();
                    if (modifiedRow["standardoption"] != null && modifiedRow["standardoption"].ToString().ToUpper() == "TRUE")
                    {
                        so = true;
                    }
                    else
                    {
                        so = false;
                    }
                    if (modifiedRow["standardinclusion"] != null && modifiedRow["standardinclusion"].ToString().ToUpper() == "TRUE")
                    {
                        si = true;
                    }
                    else
                    {
                        si = false;
                    }

                    if (modifiedRow["changeqty"] != null && modifiedRow["changeqty"].ToString().ToUpper() == "TRUE")
                    {
                        changeqty = true;
                    }
                    else
                    {
                        changeqty = false;
                    }

                    if (modifiedRow["addextradesc"] != null && modifiedRow["addextradesc"].ToString().ToUpper() == "TRUE")
                    {
                        addextradesc = true;
                    }
                    else
                    {
                        addextradesc = false;
                    }

                    if (modifiedRow["changeprice"] != null && modifiedRow["changeprice"].ToString().ToUpper() == "TRUE")
                    {
                        changeprice = true;
                    }
                    else
                    {
                        changeprice = false;
                    }
                    if (active!=active2)
                    {

                        dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminEditHDO]", new System.Data.SqlClient.SqlParameter[12]
                            {
                                new System.Data.SqlClient.SqlParameter("@StandardOption", so),
                                new System.Data.SqlClient.SqlParameter("@GeneralOption",false ),
                                new System.Data.SqlClient.SqlParameter("@StandardInclusion", si),
                                new System.Data.SqlClient.SqlParameter("@Quantity",qty),
                                new System.Data.SqlClient.SqlParameter("@ModifiedDate", "01/01/06"),
                                new System.Data.SqlClient.SqlParameter("@ModifiedBy",MetriconCommon.UserCode),
                                new System.Data.SqlClient.SqlParameter("@Active", active2),
                                new System.Data.SqlClient.SqlParameter("@ChangeQty",changeqty ),
                                new System.Data.SqlClient.SqlParameter("@AddExtraDesc", addextradesc),
                                new System.Data.SqlClient.SqlParameter("@EnterDesc","none" ),
                                new System.Data.SqlClient.SqlParameter("@ChangePrice", changeprice),
                                new System.Data.SqlClient.SqlParameter("@OptionID",Int32.Parse(modifiedRow["optionID"].ToString())),
                            }

                               );
                        //modifiedRow["ModifiedBy"] = MetriconCommon.UserCode;
                        //modifiedRow["ModifiedDate"] = DateTime.Now.ToString();
                        //this.adminSearchHomeDisplayOptionTableAdapter.Update(modifiedRow);
                    }

                }
			}
			catch (IndexOutOfRangeException ex1)
			{
                //MessageBox.Show(ex1.Message.ToString());
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message.ToString());
			}
		}

		private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
                    
			MessageBox.Show(e.Exception.Message.ToString());
		}


		private void productAreaGroupIDToolStripTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				fillToolStripButton_Click(sender, e);

		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			if (pagLookupForm == null)
				pagLookupForm = new frmPagLookup();

			pagLookupForm.ShowDialog();

			if (pagLookupForm.SelectedPAGID != null)
			{
				this.productAreaGroupIDToolStripTextBox.Text = pagLookupForm.SelectedPAGID.ToString();
				fillToolStripButton_Click(sender, e);

			}



		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{


            DataSet dss = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select * from productAreaGroup where productAreaGroupID=" + productAreaGroupIDToolStripTextBox.Text.ToString() + " and active=1");
            if (dss.Tables[0].Rows.Count > 0)
            {

                //SearchPAGForHome();

                if (!string.IsNullOrEmpty(productAreaGroupIDToolStripTextBox.Text))
                {
                    if (pagWizardForm == null)
                        pagWizardForm = new frmPAGWizard();

                    pagWizardForm.PagID = int.Parse(productAreaGroupIDToolStripTextBox.Text);
                    //Build HomeID list


                    //System.Collections.Hashtable homeIDList = new System.Collections.Hashtable();

                    //foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    //{
                    //    if (!(homeIDList.ContainsKey(row.Cells["HomeID"].Value.ToString())))
                    //    {
                    //        homeIDList.Add(row.Cells["HomeID"].Value.ToString(), "");
                    //    }

                    //}


                    //pagWizardForm.HomeIDList = homeIDList;
                    pagWizardForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please enter the PAG ID", "Message");
                }
                //SearchPAGForHome();
            }
            else
            {
                MessageBox.Show("This PAG is inactive, you can't configure it against homes!", "Message");
            }
        }


        private void loadStatusDropdown()
        {
            DataTable dtbl = CreateStatusTable();
            ComboBox cb = (ComboBox)toolStripComboBox1.Control;
            cb.DataSource = dtbl;
            cb.DisplayMember = "status";
            cb.ValueMember = "id";
            cb.SelectedValue = 1;
        }
        private DataTable CreateStatusTable()
        {
            DataTable myTab = new DataTable("statusTab");
            DataColumn dtColumn;

            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Int32");
            dtColumn.ColumnName = "id";
            // Add id Column to the DataColumnCollection.
            myTab.Columns.Add(dtColumn);

            // Create Name column.
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "status";
            // Add Name column to the table.
            myTab.Columns.Add(dtColumn);

            DataRow myDataRow = myTab.NewRow();
            myDataRow["id"] = 2;
            myDataRow["status"] = "All";
            myTab.Rows.Add(myDataRow);

            myDataRow = myTab.NewRow();
            myDataRow["id"] = 1;
            myDataRow["status"] = "Active";
            myTab.Rows.Add(myDataRow);

            myDataRow = myTab.NewRow();
            myDataRow["id"] = 0;
            myDataRow["status"] = "Inactive";
            myTab.Rows.Add(myDataRow);

            return myTab;
        }



	}
}