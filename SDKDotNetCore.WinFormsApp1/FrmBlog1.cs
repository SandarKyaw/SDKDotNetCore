using SDKDotNetCore.Shared;
using SDKDotNetCore.WinFormsApp1.Models;
using SDKDotNetCore.WinFormsApp1.Queries;

namespace SDKDotNetCore.WinFormsApp1
{
    public partial class FrmBlog1 : Form
    {
        private readonly DapperService _dapperService;
        private readonly int _blogId;
        public FrmBlog1()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.ConnectionStringBuilder.ConnectionString);
        }

        public FrmBlog1(int blogId)
        {
            InitializeComponent();
            _blogId = blogId;
            _dapperService = new DapperService(ConnectionStrings.ConnectionStringBuilder.ConnectionString);

            var model = _dapperService.QueryFirstOrDefault<BlogModel>("Select * From tbl_blog Where blogid = @BlogId",
                new { BlogId = _blogId });

            txtTitle.Text = model.BlogTitle;
            txtAuthor.Text = model.BlogAuthor;
            txtContent.Text = model.BlogContent;

            btnSave.Visible = false;
            btnUpdate.Visible = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel();
                blog.BlogTitle = txtTitle.Text.Trim();
                blog.BlogAuthor = txtAuthor.Text.Trim();
                blog.BlogContent = txtContent.Text.Trim();

                int result = _dapperService.Execute(BlogQuery.BlogCreate, blog);
                string message = result > 0 ? "Saving Successful." : "Saving Failed.";

                var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(message, "Blog", MessageBoxButtons.OK, messageBoxIcon);
                if (result > 0)
                    ClearControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }


        private void ClearControl()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtContent.Clear();

            txtTitle.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var item = new BlogModel
                {
                    BlogId = _blogId,
                    BlogTitle = txtTitle.Text.Trim(), //trim - delete the space 
                    BlogAuthor = txtAuthor.Text.Trim(),
                    BlogContent = txtContent.Text.Trim(),
                };

                string query = @"UPDATE [dbo].[Tbl_Blog]
                   SET [BlogTitle] = @BlogTitle
                      ,[BlogAuthor] = @BlogAuthor
                      ,[BlogContent] = @BlogContent
                 WHERE [BlogId] = @BlogId";
                
                int result = _dapperService.Execute(query, item);
                string message = result > 0 ? "Updating Successful." : "Updating Failed.";
                MessageBox.Show(message);

                this.Close();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
