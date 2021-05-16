
namespace AlphabetBook
{
    public class FourPathUz : PlayerTracing
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

                    PathCompleted(index);

                    break;
                case 1:

                    isPathCompleted = CheckPath(3, 6);

                    PathCompleted(index);

                    break;
                case 2:

                    isPathCompleted = CheckPath(4, 7);

                    PathCompleted(index);

                    break;
                case 3:

                    isPathCompleted = CheckPath(10, 16);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }

    }
}


