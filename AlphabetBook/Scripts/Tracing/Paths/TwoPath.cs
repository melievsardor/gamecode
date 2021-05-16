
namespace AlphabetBook
{
    public class TwoPath : PlayerTracing
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

                    isPathCompleted = CheckPath(4, 7);

                    if (isPathCompleted)
                    {
                        NextPath();

                        ActivePath();

                        ShowPathsHelper(0);
                    }

                    break;
                case 1:

                    isPathCompleted = CheckPath(5, 10);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }
    }

}

