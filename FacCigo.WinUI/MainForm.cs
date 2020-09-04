using FacCigo.Samples;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Volo.Abp.DependencyInjection;

namespace FacCigo
{
    public partial class MainForm : Form, ISingletonDependency
    {
        ISampleAppService sampleAppService;
        public MainForm(ISampleAppService sampleAppService)
        {
            InitializeComponent();
            this.sampleAppService = sampleAppService;
            
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Application.ExitThread();
        }

        private void menuStrip_ItemClickedAsync(object sender, ToolStripItemClickedEventArgs e)
        {
            Task<SampleDto> dto = this.sampleAppService.GetAsync();
            MessageBox.Show(dto.Result.Value.ToString());
          
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
