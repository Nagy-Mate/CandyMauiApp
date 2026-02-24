namespace CandyShop
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            Routing.RegisterRoute("EditPage", typeof(EditPage));
            Routing.RegisterRoute("SellCandyPage", typeof(SellCandyPage));
            Routing.RegisterRoute("ReportPage", typeof(ReportPage));
            InitializeComponent();
        }
    }

}

