using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameCenterProject.Projects
{
    /// <summary>
    /// Interaction logic for projectPresentationPage.xaml
    /// </summary>
    public partial class projectPresentationPage : Window
    {
        private Window? currentProject;
        public projectPresentationPage()
        {
            InitializeComponent();
        }

        public void OnStart(string title, string projectDescription, ImageSource imageSource, Window project)
        {
            addUserTitle.Content = title;
            ProjectDescription.Text = projectDescription;
            ProjectImage.Source = imageSource;
            currentProject = project;
        }

        private void LaunchProject(object sender, MouseButtonEventArgs e)
        {            
            Close();
            currentProject!.ShowDialog();
            currentProject!.Close();
        }
    }
}
