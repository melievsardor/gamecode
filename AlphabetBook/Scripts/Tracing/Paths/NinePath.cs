
namespace AlphabetBook
{
    public class NinePath : PlayerTracing
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

                    isPathCompleted = CheckPath(3, 7);

                    PathCompleted(index);

                    break;
                case 1:

                    isPathCompleted = CheckPath(3, 7);

                    PathCompleted(index);

                    break;
                case 2:

                    isPathCompleted = CheckPath(3, 7);

                    PathCompleted(index);

                    break;
                case 3:

                    isPathCompleted = CheckPath(6, 8);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }

    }

}

