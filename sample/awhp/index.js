const puppeteer = require("puppeteer");
const axios = require("axios");
const FormData = require("form-data");

(async() => {
    const width = 1200;
    const height = 740;
    const svcurl = "http://localhost:60417/";

    const browser = await puppeteer.launch({
        headless: false
    });
    const page = await browser.newPage();
    page.setViewport({width, height});

    const response = await axios.post(`${svcurl}processes`);
    const processId = response.data.id;

    async function checkDifference(page, imagekey){
        const image = await page.screenshot({
            fullPage: true,
            type: "jpeg"
        });
    
        const form = new FormData();
        form.append("process", processId);
        form.append("key", imagekey);
        form.append("actual", image, {
            filepath: "image.jpg",
            contentType: "image/jpg"
        });
        
        const result = await axios.put(`${svcurl}diff`, form, {
            headers: form.getHeaders()
        });
        return result.data;
    }

    //トップページ
    await page.goto("https://www.archway.co.jp/");
    console.log(await checkDifference(page, "toppage"));
    //メッセージページ
    await page.goto("https://www.archway.co.jp/message/");
    console.log(await checkDifference(page, "messagepage"));

    //ミッションページ
    await page.goto("https://www.archway.co.jp/mission/");
    console.log(await checkDifference(page, "missionpage"));
    
    browser.close();
})();