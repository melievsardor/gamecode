

namespace AlphabetBook
{
    public class FivePathUz : PlayerTracing
    {

        protected override void Start()
        {
            base.Start();


            brushPrefabs.transform.localScale = new UnityEngine.Vector3(0.15f, 0.15f, 0.15f);
        }

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

                    isPathCompleted = CheckPath(3, 6);

                    PathCompleted(index);

                    break;
                case 1:

                    isPathCompleted = CheckPath(1, 3);

                    PathCompleted(index);

                    break;
                case 2:

                    isPathCompleted = CheckPath(2, 5);

                    PathCompleted(index);

                    break;
                case 3:

                    isPathCompleted = CheckPath(2, 5);

                    PathCompleted(index);

                    break;
                case 4:

                    isPathCompleted = CheckPath(1, 3);

                    PathCompleted(index);

                    break;
                case 5:

                    isPathCompleted = CheckPath(1, 3);

                    PathCompleted(index);

                    break;
                case 6:

                    isPathCompleted = CheckPath(2, 5);

                    PathCompleted(index);

                    break;
                case 7:

                    isPathCompleted = CheckPath(2, 5);

                    PathCompleted(index);

                    break;
                case 8:

                    isPathCompleted = CheckPath(2, 5);

                    PathCompleted(index);

                    break;

                case 9:

                    isPathCompleted = CheckPath(5, 10);

                    if (isPathCompleted)
                        CompletedTracing();

                    break;
            }
        }





    }
}


