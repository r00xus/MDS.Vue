<template>
    <h4 v-if="id != 0">Изменить управление</h4>
    <h4 v-if="id == 0">Создать управление</h4>

    <div style="margin-top:20px;">
        <div class="form-group">
            <label for="name">Название</label>
            <input v-model="orgUnit.name" type="text" class="form-control" id="name" name="name">
            <div class="error">
                {{ errors.name }}
            </div>
        </div>
        <p style="text-align:right;">
            <button @click="save" class="btn btn-success">Сохранить</button>
            <button @click="back" class="btn btn-secondary" style="margin-left:10px">Назад к списку</button>
        </p>
    </div>
</template>

<script>
    import router from '../router'
    import axios from 'axios'
    import { useRoute } from 'vue-router'

    export default {
        name: "DirectionForm",
        data() {
            return {
                id: null,
                orgUnit: {
                    id: 0,
                    name: null,
                    type: 0
                },
                errors: {
                    name: null
                }
            }
        },
        methods: {
            back() {
                router.push({ name: 'directions' });
            },
            getDirection(id) {

                axios.get('/api/OrgUnits/' + id)
                    .then((response) => {
                        this.orgUnit = response.data;
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            save() {

                if (!this.isValid()) return;

                if (this.id == 0)
                    this.create()
                else
                    this.edit(this.id)
            },
            create() {

                axios.post('/api/OrgUnits/', this.orgUnit)
                    .then(() => {
                        this.back();
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            edit(id) {

                axios.put('/api/OrgUnits/' + id, this.orgUnit)
                    .then(() => {
                        this.back();
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            isValid() {

                this.errors.name = null;

                if (this.orgUnit.name == "" || this.orgUnit.name == null) {
                    this.errors.name = "Заполните поле!";
                    return false;
                }

                return true;
            }
        },
        mounted() {

            const route = useRoute()

            this.id = route.params.id === undefined ? 0 : route.params.id;

            if (this.id != 0)
                this.getDirection(this.id);

        }
    }
</script>