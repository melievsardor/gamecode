
namespace AlphabetBook
{
    public class QPath : PlayerTracing
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

                    isPathCompleted = CheckPath(6, 8);

                    PathCompleted(index);

                    break;
                case 1:

                    isPathCompleted = CheckPath(6, 10);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }


    }

}

