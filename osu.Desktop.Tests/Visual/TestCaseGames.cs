// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Screens.Games;

namespace osu.Desktop.Tests.Visual
{
    internal class TestCaseGames : OsuTestCase
    {
        public override string Description => @"Games screen";

        public TestCaseGames()
        {
            Add(new GamesScreen());
        }
    }
}
