using Reprizo.Areas.Admin.ViewModels.BestWorker;
using Reprizo.Areas.Admin.ViewModels.Repair;
using Reprizo.Areas.Admin.ViewModels.Team;

namespace Reprizo.Areas.Admin.ViewModels.About
{
    public class AboutVM
    {
        public RepairVM Repair { get; set; }
        public List<TeamVM> Teams { get; set; }
        public BestWorkerVM Best { get; set; }
    }
}
