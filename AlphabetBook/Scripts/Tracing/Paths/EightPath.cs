
namespace AlphabetBook
{
    public class EightPath : PlayerTracing
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

                    isPathCompleted = CheckPath(4, 7);

                    PathCompleted(index);

                    break;
                case 2:

                    isPathCompleted = CheckPath(4, 7);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }


    }
}


