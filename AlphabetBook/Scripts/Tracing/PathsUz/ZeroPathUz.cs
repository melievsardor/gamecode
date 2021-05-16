

namespace AlphabetBook
{
    public class ZeroPathUz : PlayerTracing
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

                    isPathCompleted = CheckPath(8, 12);

                    PathCompleted(index);

                    break;
                case 1:

                    isPathCompleted = CheckPath(2, 5);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }
    }
}


