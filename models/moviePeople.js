module.exports = (sequelize, Sequelize) => {
    const MoviePeople = sequelize.define("moviePeople")

    MoviePeople.associate = models => {
        models.moviePeople.belongsTo(models.roles, {
            foreignKey: 'roleId',
            as: 'role'
        })
    }

    return MoviePeople
}