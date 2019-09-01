using osu.Game.Online.API;

namespace osu.Game.Screens.Evast.ApiHelper
{
    public class GetAPIDataRequest : APIRequest
    {
        private readonly string target;

        public string Result => WebRequest.ResponseString;

        public GetAPIDataRequest(string target)
        {
            this.target = target;
            base.Success += onSuccess;
        }

        private void onSuccess() => Success?.Invoke(Result);

        public new event APISuccessHandler<string> Success;

        protected override string Target => target;
    }
}
