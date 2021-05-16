

namespace AlphabetBook
{
    public class GPathUz : PlayerTracing
    {

        protected override void EndLine()
        {
            base.EndLine();

            if (index == 0)
            {
                isPathCompleted = CheckPath(10, 13);

                if (isPathCompleted)
                    CompletedTracing();
            }
        }


    }
}


