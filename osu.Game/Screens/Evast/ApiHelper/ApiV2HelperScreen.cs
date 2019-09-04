﻿using Newtonsoft.Json;

namespace osu.Game.Screens.Evast.ApiHelper
{
    public class ApiV2HelperScreen : WebHelperScreen
    {
        protected override string CreateUri() => "https://osu.ppy.sh/api/v2/";

        protected override void OnRequestSuccess(string result)
        {
            var parsed = JsonConvert.DeserializeObject(result);
            Text.AddText(JsonConvert.SerializeObject(parsed, Formatting.Indented));
        }
    }
}
