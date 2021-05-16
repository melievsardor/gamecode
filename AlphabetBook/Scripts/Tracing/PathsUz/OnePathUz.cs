﻿
namespace AlphabetBook
{
    public class OnePathUz : PlayerTracing
    {

        protected override void ActivePath()
        {
            base.ActivePath();

            isPathCompleted = false;

            ShowCollider(index);
        }

        protected override void EndLine()
        {
            base.EndLine();
            switch (index)
            {
                case 0:

                    isPathCompleted = CheckPath(13, 17);

                    PathCompleted(index);

                    break;
                case 1:

                    isPathCompleted = CheckPath(4, 7);

                    PathCompleted(index);

                    break;
                case 2:

                    isPathCompleted = CheckPath(4, 7);

                    PathCompleted(index);

                    break;
                case 3:

                    isPathCompleted = CheckPath(2, 5);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }


    }
}
