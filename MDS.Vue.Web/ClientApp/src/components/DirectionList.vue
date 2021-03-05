<template>
    <h4 style="margin-bottom:20px;">Управления</h4>
    <p>
        <button @click="reload" class="btn btn btn-primary">Обновить</button>
        <button @click="create" class="btn btn btn-success" style="margin-left: 10px">Создать</button>
    </p>
    <table class='table table-striped'>
        <thead>
            <tr>
                <th>Управление</th>
                <th style="text-align:right;">Отделов</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="direction of directions" v-bind:key="direction">
                <td>
                    <router-link :to="{ name: 'departments', params : { id: direction.id } }"> {{ direction.name }}</router-link>
                </td>
                <td style="text-align:right;">
                    {{ direction.departmentsCount }}
                </td>
                <td width="250" style="text-align:right;">
                    <button @click="edit(direction.id)" class="btn btn-info">Изменить</button>
                    <button class="btn btn-danger" style="margin-left:10px">Удалить</button>
                </td>
            </tr>
        </tbody>
    </table>

</template>


<script>
    import axios from 'axios'
    import router from '../router'

    export default {
        name: "DirectionList",
        data() {
            return {
                directions: []
            }
        },
        methods: {

            getDirections() {

                axios.get('/api/list/direction')
                    .then((response) => {
                        this.directions = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            create() {
                router.push({
                    name: 'createDirection'
                });
            },
            edit(id) {
                router.push({
                    name: 'editDirection',
                    params: {
                        id: id
                    }
                });
            },
            reload() {
                this.getDirections();
            }
        },
        mounted() {
            this.getDirections();
        }
    }
</script>