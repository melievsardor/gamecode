
namespace AlphabetBook
{
    public class IPathUz : PlayerTracing
    {

        protected override void EndLine()
        {
            base.EndLine();

            if (index == 0)
            {
                isPathCompleted = CheckPath(4, 7);

                if (isPathCompleted)
                    CompletedTracing();
            }
        }

    }
}


