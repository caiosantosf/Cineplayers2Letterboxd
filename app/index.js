const express = require('express')
const path = require('path')

require('dotenv').config()

const scraping = require('./scraping/index')

const app = express();
const port = process.env.PORT || "3000";

app.set('view engine', 'ejs')
app.set('views', path.join(__dirname, 'views'));

app.use(require('./api/routes/index'));

app.listen(port, () => {
    console.log(`Listening to requests on http://localhost:${port}`);
});

const baseUrl = 'https://cineplayers.com'
const cpUser = 12688

//scraping.importWatchList(baseUrl, cpUser)
