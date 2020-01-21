const express = require('express')
const path = require('path')

require('dotenv').config()

const scraping = require('./services/scraping')

const baseUrl = 'https://cineplayers.com'
const cpUser = 12688

const app = express();
const port = process.env.PORT || "3000";

app.set('view engine', 'ejs')
app.set('views', path.join(__dirname, 'views'));

app.use(require('./api/routes/index'));

app.listen(port, () => {
    console.log(`Listening to requests on http://localhost:${port}`);
});

//scraping.importWatchList(baseUrl, cpUser)
