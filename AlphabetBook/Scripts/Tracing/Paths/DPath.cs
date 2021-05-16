﻿
namespace AlphabetBook
{
    public class DPath : PlayerTracing
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

                    isPathCompleted = CheckPath(6, 9);

                    PathCompleted(index);

                    break;
                case 1:

                    isPathCompleted = CheckPath(3, 5);

                    PathCompleted(index);

                    break;
                case 2:

                    isPathCompleted = CheckPath(3, 5);

                    PathCompleted(index);

                    break;
                case 3:

                    isPathCompleted = CheckPath(3, 5);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }



    }

}

