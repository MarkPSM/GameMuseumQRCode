using System.Collections;
using TMPro;
using UnityEngine;
using ZXing;

public class QRCodeScan : MonoBehaviour
{
    [Header("Componentes")]
    public GameObject Image;
    public GameObject BGDescription;
    public TextMeshPro Description;

    [Header("Info")]
    public Sprite[] images;
    public string[] descriptions;

    private string gameName;

    private bool isScanning = true;

    void Start()
    {
        if (Image != null)
            Image.gameObject.SetActive(false);

        if (Description != null)
            Description.gameObject.SetActive(false);

        if(BGDescription != null)
            BGDescription.gameObject.SetActive(false);

        StartCoroutine(ScanQRCodeRoutine());
    }

    void AbrirDescricao()
    {
        if (!string.IsNullOrEmpty(gameName))
        {
            BGDescription.gameObject.SetActive(true);
            Description.gameObject.SetActive(true);

            SpriteRenderer sr = Image.GetComponent<SpriteRenderer>();

            if (descriptions != null)
            {
                switch (gameName)
                {
                    case "Astrobot":
                        sr.sprite = images[0];
                        Description.SetText(descriptions[0]);
                        break;

                    case "BaldursGate":
                        sr.sprite = images[1];
                        Description.SetText(descriptions[1]);
                        break;

                    case "EldenRing":
                        sr.sprite = images[2];
                        Description.SetText(descriptions[2]);
                        break;

                    case "GodOfWar":
                        sr.sprite = images[3];
                        Description.SetText(descriptions[3]);
                        break;

                    case "ItTakesTwo":
                        sr.sprite = images[4];
                        Description.SetText(descriptions[4]);
                        break;

                    case "Overwatch":
                        sr.sprite = images[5];
                        Description.SetText(descriptions[5]);
                        break;

                    case "Sekiro":
                        sr.sprite = images[6];
                        Description.SetText(descriptions[6]);
                        break;

                    case "TheLastOfUs":
                        sr.sprite = images[7];
                        Description.SetText(descriptions[7]);
                        break;

                    case "Witcher3":
                        sr.sprite = images[8];
                        Description.SetText(descriptions[8]);
                        break;

                    case "Zelda":
                        sr.sprite = images[9];
                        Description.SetText(descriptions[9]);
                        break;
                }
            }
        }
    }

    public void VoltarScanner()
    {
        isScanning = true;

        gameName = null;

        Image.gameObject.SetActive(false);
        Description.gameObject.SetActive(false);
        BGDescription.gameObject.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(ScanQRCodeRoutine());
    }

    IEnumerator ScanQRCodeRoutine()
    {
        //Espera para garantir a ativação da Câmera
        yield return new WaitForSeconds(2f);

        while (isScanning)
        {
            yield return new WaitForEndOfFrame();
            Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();

            if (screenshot != null)
            {
                IBarcodeReader reader = new BarcodeReader();

                var result = reader.Decode(screenshot.GetPixels32(), screenshot.width, screenshot.height);


                if (result != null)
                {
                    Debug.Log("QR Code Detectado: " + result.Text);

                    isScanning = false;

                    gameName = result.Text;

                    Transform cam = Camera.main.transform;
                    Vector3 DescriptionSpawnPosition = cam.position + cam.forward * 0.5f;
                    Vector3 spawnPosition = cam.position + cam.forward * 0.5f;

                    Vector3 ImageSpawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + 0.6f, spawnPosition.z);

                    Image.transform.position = ImageSpawnPosition;
                    BGDescription.transform.position = DescriptionSpawnPosition;


                    Image.gameObject.SetActive(true);
                    BGDescription.gameObject.SetActive(true);
                    Description.gameObject.SetActive(true);

                    AbrirDescricao();
                }

                Destroy(screenshot);

            }
            yield return new WaitForSeconds(1f);
        }
    }
}
