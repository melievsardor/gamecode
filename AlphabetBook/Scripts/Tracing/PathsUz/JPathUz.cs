

namespace AlphabetBook
{
    public class JPathUz : PlayerTracing
    {

        protected override void EndLine()
        {
            base.EndLine();

            if (index == 0)
            {
                isPathCompleted = CheckPath(5, 7);

                if (isPathCompleted)
                    CompletedTracing();
            }
        }

    }

}

