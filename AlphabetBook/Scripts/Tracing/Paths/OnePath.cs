
namespace AlphabetBook
{
    public class OnePath : PlayerTracing
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

                    isPathCompleted = CheckPath(10, 14);

                    PathCompleted(index);

                    break;
                case 1:

                    isPathCompleted = CheckPath(1, 4);

                    if (isPathCompleted)
                        CompletedTracing();
                    break;
            }
        }

       


    }
}


