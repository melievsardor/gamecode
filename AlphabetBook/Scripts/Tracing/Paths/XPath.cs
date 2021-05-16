
namespace AlphabetBook
{
    public class XPath : PlayerTracing
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

                    isPathCompleted = CheckPath(6, 9);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }

    }

}

