
namespace AlphabetBook
{
    public class VPath : PlayerTracing
    {

        protected override void ActivePath()
        {
            base.ActivePath();

            isPathCompleted = false;
        }

        protected override void EndLine()
        {
            base.EndLine();
            switch (index)
            {
                case 0:

                    isPathCompleted = CheckPath(5, 7);

                    PathCompleted(index);

                    break;
                case 1:

                    isPathCompleted = CheckPath(10, 14);

                    if (isPathCompleted)
                        CompletedTracing();
                    break;

            }
        }

    }

}

