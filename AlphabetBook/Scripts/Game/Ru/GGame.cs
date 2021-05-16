

namespace AlphabetBook
{
    public class GGame : FindBase
    {

        protected override void SetQuestion()
        {
            base.SetQuestion();

            imageIndex += 4;

            int[] randoms = Constants.GetRandomIndex(images.Length);

            images[randoms[0]].sprite = sprites[imageIndex];

            //images[randoms[0]].rectTransform.SetHeight(spritesSize[imageIndex].y);
            //images[randoms[0]].rectTransform.SetWidth(spritesSize[imageIndex].x);

            buttons[randoms[0]].onClick.AddListener(() => OnClickItem(false, buttonTransform[randoms[0]], buttons[randoms[0]]));

            images[randoms[1]].sprite = sprites[imageIndex + 1];

            //images[randoms[1]].rectTransform.SetHeight(spritesSize[imageIndex + 1].y);
            //images[randoms[1]].rectTransform.SetWidth(spritesSize[imageIndex + 1].x);

            buttons[randoms[1]].onClick.AddListener(() => OnClickItem(false, buttonTransform[randoms[1]], buttons[randoms[1]]));

            images[randoms[2]].sprite = sprites[imageIndex + 2];

            //images[randoms[2]].rectTransform.SetHeight(spritesSize[imageIndex + 2].y);
            //images[randoms[2]].rectTransform.SetWidth(spritesSize[imageIndex + 2].x);

            buttons[randoms[2]].onClick.AddListener(() => OnClickItem(false, buttonTransform[randoms[2]], buttons[randoms[2]]));

            images[randoms[3]].sprite = sprites[imageIndex + 3];

            //images[randoms[3]].rectTransform.SetHeight(spritesSize[imageIndex + 3].y);
            //images[randoms[3]].rectTransform.SetWidth(spritesSize[imageIndex + 3].x);

            buttons[randoms[3]].onClick.AddListener(() => OnClickItem(true, buttonTransform[randoms[3]], buttons[randoms[3]]));

            ShowItems();
        }

    }
}


