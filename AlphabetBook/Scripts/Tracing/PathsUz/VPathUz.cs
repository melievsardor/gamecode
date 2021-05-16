

namespace AlphabetBook
{
    public class VPathUz : PlayerTracing
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

                    isPathCompleted = CheckPath(5, 8);

                    PathCompleted(index);

                    break;
                case 1:

                    isPathCompleted = CheckPath(5, 8);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }



    }
}


