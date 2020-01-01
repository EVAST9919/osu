using System;
using osu.Game.Online.API;

namespace osu.Game.Screens.Evast.ApiHelper
{
    public class GetAPIDataRequest : APIRequest
    {
        private readonly string target;
        private readonly Uri uri;

        public string Result => WebRequest.GetResponseString();

        public GetAPIDataRequest(string target, Uri uri)
        {
            this.target = target;
            this.uri = uri;
            base.Success += onSuccess;
        }

        private void onSuccess() => Success?.Invoke(Result);

        public new event APISuccessHandler<string> Success;

        protected override string Target => target;

        protected override string Uri => $@"{uri}{Target}";
    }
}
